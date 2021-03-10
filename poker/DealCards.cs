using System;
using System.Collections.Generic;
using System.Linq;

namespace poker
{
    public class DealCards
    {
        public int[,] GetHandCards(int[,] handsCardArray)
        {
            //staticにした場合には呼び出して使用する時に
            //DealCards.GetHandCards() みたいな書き方で使用する

            //ランダムで生成したカードのIDを管理するList
            var HandsCardList = new List<int>();

            //return用多次元配列
            //var handsCardArrays = new int[5,3];     // [ID, divide, remainder]

            bool IsDuplicatedCard = false;
            const int maxHandsCards = 5;

            Random r1 = new Random();

            //手札を配る
            for (int i = 0; i < maxHandsCards; i++)
            {
                //ランダムにてカードを選ぶ
                int cardId = r1.Next(1, 54);   //1 〜 53

                if (i >= 2)
                {
                    //リストの中身が既に重複していないかをチェック
                    //IsDuplicatedCard = CheckProvideCard(HandsCardList, cardId);
                    //if文で問題なければリストに追加、ダメならもう一度処理

                    HandsCardList.Add(cardId);
                }
                else
                {
                    HandsCardList.Add(cardId);
                    //handsArray[i] = cardId;
                }

            }
            //1~13, 14~26, 27~39, 40~52

            //デバッグ用
            //int num = 11;
            //HandsCardList[0] = num;
            //HandsCardList[1] = num + 13;
            //HandsCardList[2] = num + 13 * 2;
            //HandsCardList[3] = 3;
            //HandsCardList[4] = 16;




            //照準に並び替え(Listのメソッド)
            HandsCardList.Sort();




            handsCardArray = MakeCardDetailsByArray(HandsCardList);

            //(デバッグ用)ジョーカーを入れる
            //handsCardArray[4, 0] = 53;
            //handsCardArray[4, 1] = 4;
            //handsCardArray[4, 2] = 1;

            return handsCardArray;
        }

        //private int CheckProvideCard(List<int> handsArray, int cardId)
        //{
        //    int result = 0;

        //    return result;
        //}

        private bool CheckProvideCard(List<int> handList, int cardId)
        {
            bool result = false;


            return result;
        }

        //listを引数にとって多次元配列にして戻す
        private int[,] MakeCardDetailsByArray(List<int> cardId)
        {
            const int CARDLENGTH = 13;
            const int MAXHANDSCARDS = 5;
            int divided;
            int remainder;

            var handsCardArrays = new int[5, 3];     // [ID, divide, remainder]

            //引数のListの要素を商と余に分解して配列に入れる
            for (int i = 0; i < MAXHANDSCARDS; i++)
            {
                divided = cardId[i] / CARDLENGTH;
                remainder = cardId[i] % CARDLENGTH;

                //Kingかどうか判定
                if (remainder == 0)
                {
                    remainder = 13;
                    divided--;
                }

                handsCardArrays[i, 0] = cardId[i];
                handsCardArrays[i, 1] = divided;
                handsCardArrays[i, 2] = remainder;
            }

            return handsCardArrays;
        }
    }
}
