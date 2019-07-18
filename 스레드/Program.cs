using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/*
 * 프로그램은 여러개의 프로세스를 가지고 있음
 * 프로세스 : 여러개의 스레드를 가지고 프로그램을 실행하는 연산작업 단위
 * 프로세스간에 데이터 교환이 가능하나 파이프라인을 통해서 한정적으로 교환
 * 쓰레드 : 코드 실행을 CPU에 요청하는 단위
 * 프로그램이 실행되던 Main 함수가 호출되고, Main함수를 동작시키는 쓰레드를 메인 쓰레드라 부름. 추가적인 연산을 위해 새로운 쓰레드를 생성 시 생성된 쓰레드를 서브쓰레드라 부름.
 * 메인쓰레드 외에 다른 서브 쓰레드를 이용해 파일다운로드/ 음악재생/ 서버요청/ 등을 처리하는 것이 보편적임.
 * 윈도우 폼에서 GUI환경 관리를 메인쓰레드가 담당함.
 * 
 * C#에서 쓰레드 생성 방법
 * 1) Thread 클래스 객체 생성
 * 2) ThreadPool 정적 함수 사용
 * 3) Task 클래스 객체 생성
 * 
 * 스레드 기본 예제
 */

namespace 스레드
{
    //서브스레드에게 전송할 값을 모아놓은 클래스 정의
    class Data
    {
        public int min, max, number;

        public Data(int min, int max, int number)
        {
            this.min = min; // 객체 || 매개변수
            this.max = max;
            this.number = number;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //서브 스레드 생성 및 시작
            /*ThreadPool :  매개변수 1개를 사용하는 함수를 서브스레드로 생성할때 사용할 수 있는 클래스. 서브 스레드를 생성할때 QueueUserWorkItem함수로 서브스레드 생성을 요청 할 수 있음.
             * CPU의 최대 스레드 최대 생성 개수는 프로세서 * 250개 까지 생성할 수 있음.
             * ThreadPool로 다수의 서브스레드 생성 요청 시, 프로세서 *2개는 즉각적으로 생성시켜주나, 초과하는 서브 스레드 생성 요청은 초당 2개씩 생성함.
             * ex) 프로세서 4개이고 서브스레드 20개 생성요청
             * -> 8개의 서브스레드는 바로 생성, 나머지 12개는 스레드는 6초에 걸쳐 생성
             * 기존의 서브스레드가 종료된 후 다시 서브스레드 생성 요청을 하면 기존의 서브스레드를 재활용하도록 처리함.
             */

            /*for( int i = 0; i < 30; i++)
            {
                //서브스레드에 객체를 전달 및 생성
                Data data = new Data(i * 1, 50 * i, i);
                ThreadPool.QueueUserWorkItem(thread_func, data);
         
                //일반적인 변수값을 서브스레드에 전달
                //ThreadPool.QueueUserWorkItem(thread_func, i);
                //매개변수를 사용하지않고 서브스레드 생성
                //ThreadPool.QueueUserWorkItem(thread_func)
            }*/

            //Task 객체 생성 및 서브스레드 시작 -> Thread 제어 가능
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 20; i++)
            {
                //매개변수 있는 서브스레드 생성

                //서브스레드 한개 생성
                //Task task = new Task(new Action(thread_func2)); //Action은 매개변수가 없는 메소드?를 넣어야한다.
                //task.Start(); //서브스레드 시작
                tasks.Add(new Task(new Action(thread_func2)));
                tasks[i].Start();
            }
            
            //Task.WaitAll : 인자값에 있는 모든 Task 객체가 종료될때까지 대기
            Task.WaitAll(tasks.ToArray());
            //task.Wait(); //서브스레드가 종료될때까지 대기

            //메인 스레드를 유지 - 무한반복
            while (true)
            {
                Console.WriteLine("메인스레드 동작중");
                Thread.Sleep(1000);
            }
        }
        //서브 스레드로 동작시킬 함수 정의
        static void thread_func2()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("서브스레드 동작 {0}", i);
                Thread.Sleep(500);
            }
            Console.WriteLine("서브스레드 종료");
        }

        static void thread_func(object a) //비동기 델리게이트 반환타입 이건 따로 공부해야한다.
        {
            Data data = (Data)a;
            int number = data.number;

            //int number = (int)a;
            for (int i = data.min; i < data.max; i++)
            {
                Console.WriteLine("{1}번 {0} 서브스레드 동작중", i, number);
                Thread.Sleep(500); // 단위 : 1000ms = 1s

            }

            //서브스레드 예제 경마
        }
    }
}
