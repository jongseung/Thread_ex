using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/*
 * 서브스레드를 기반으로한 경주 프로그램
 * 기능
 * 메인스레드는 서브스레드가 모두 종료할때까지 도달지점을 화면에 출력
 * 서브스레드는 골인지점에 도착하면 등수를 저장하는 변수에 등번호 입력하기.
 * 모든 서브스레드가 종료하면 등수를 출력.
 * 
 */

namespace 스레드
{
    //선수 정의
    class Player
    {
        //정적변수로 순위를 저장하는 배열과 인덱스저장 변수 선언
        public static int[] rank;
        public static int rank_index;
        //현재 지점, 등번호, 종료 여부
        public int distance, number;
        public bool isFinish;
        //서브스레드로 동작할 달리기 함수 정의
        public void run()
        {
            isFinish = false;
            //달리기
            //Random : 난수를 생성할 때 사용하는 클래스
            Random random = new Random();
            //Random.Next(최대값) : 0~ 최대값 사이의 임의의 숫자를 반환하는 함수
            for(distance = 0; distance< 1000; distance += random.Next(5))
            {
                Thread.Sleep(500);
            }

            isFinish = true;
            rank[rank_index] = number;
            rank_index ++;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //시작 종료 여부 다 종료하면 결과 출력
        }
    }
}