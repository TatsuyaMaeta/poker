using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace poker
{
    public class CheckRole
    {
        //markerList.Distinct<int>().Count<int>();した時のカウント数
        //1...全て同じ→フラッシュ

        const int MAXHANDSCARDS = 5;
        
        public static int CheckHands(int[,] handsCardArray, bool asterFlg, string[] baseCardsArray)
        {

            List<int> handsID = new List<int>();
            List<int> handsMark = new List<int>();
            List<int> handsNumber = new List<int>();
            List<string> handsNumberAlph = new List<string>();

            int resultHandsRole = 0;

            for (int i = 0; i < MAXHANDSCARDS; i++)
            {
                handsID.Add(handsCardArray[i, 0]);
                handsMark.Add(handsCardArray[i, 1]);
                handsNumber.Add(handsCardArray[i, 2]);
                handsNumberAlph.Add(baseCardsArray[handsCardArray[i, 2] - 1]);
            }
            handsID.Sort();
            handsMark.Sort();
            handsNumber.Sort();


            Dictionary<string, int> DictHandMatch = new Dictionary<string, int>
            {
                {"MatchMark", handsMark.Distinct().Count()} ,
                {"MatchNumer", handsNumber.Distinct().Count()}
            };

            List<Dictionary<string, int>> checkMaxDuplicate = CheckMaxDuplicate(handsNumber);
            //List <Dictionary<string, int>> checkMaxDuplicate = CheckMaxDuplicate(handsNumber);
            int MaxDuplicate = 0;

            Console.WriteLine("List Keys = "+ checkMaxDuplicate[0].Keys);
            //for (int i = 0; i < checkMaxDuplicate.Count; i++)
            //{
            //    int i = checkMaxDuplicate[i].Keys;
                
            //}

            
            //checkMaxDuplicateのcountが2の場合valueを比較して手札の役を判定
            //最大値:2 => IsTwoPair || 最大値:3 => FullHouse 

            resultHandsRole = 1;




            resultHandsRole = IsStrait(handsNumber, asterFlg);
            resultHandsRole = IsStraitFlush(handsNumber, handsMark, asterFlg);
            // 1... flush,  StraitFlush,RoyalStraitFlushの可能性あり
            // 2... flush,  StraitFlush,RoyalStraitFlushの可能性あり(joker)

            // 2... FullHouse, FourCard, FiveCard(joker)
            // 3... ThreeCard, TwoPair, FullHouse(joker)
            // 4... OnePair, ThreeCard(joker)
            // 5... NoPair, OnePair(joker)  || Strait, StraitFlush, RoyalStraitFlushの可能性あり

            if (DictHandMatch["MatchMark"] == 1)
            {
                //resultHandsRole = IsRoyalStraitFlush();
                //resultHandsRole = IsStraitFlush();
                resultHandsRole = IsFlush(handsID, handsMark, handsNumber, asterFlg);
            }
            else if(DictHandMatch["MatchNumer"] == 2)
            {
                if (true)
                {

                }
                //resultHandsRole = IsFullHouse
                //resultHandsRole = IsFourCard
                //resultHandsRole = IsFiveCard
            }
            else if (DictHandMatch["MatchNumer"] == 3)
            {
                //resultHandsRole = IsThreeCard();
                resultHandsRole = IsTwoPair(handsNumber, asterFlg);
            }
            else if (DictHandMatch["MatchNumer"] == 4)
            {

            }
            else if (DictHandMatch["MatchNumer"] == 5)
            {
                resultHandsRole = IsOnePair(handsNumber, asterFlg);
                resultHandsRole = IsNoPair(handsID, handsMark, handsNumber, asterFlg);
            }

            //int role_point = (asterFlg, DictHandMatch["MatchMark"], DictHandMatch["MatchNumer"]) switch
            //{
            //    //ジョーカーあり
            //    (true, _, 1) => 1,
            //    (true, _, 2) => 3,
            //    (true, _, 3) => 4,

            //    //ジョーカーなしのツーペア
            //    (false, true, _) => 2,

            //    //ジョーカーなしの各手札組み合わせ(ツーペア以外)
            //    (false, false, 1) => 0,
            //    (false, false, 2) => 1,
            //    (false, false, 3) => 3,
            //    (false, false, 4) => 4,

            //    //明示的破棄
            //    _ => throw new NotImplementedException(),
            //};





            return resultHandsRole;
        }


        //ロイヤルストレートフラッシュの条件
        //絵柄が全て同じ
        //数字が10~K,Aである
        private static int IsStraitFlush(List<int> handsNumber,List<int> handsMark , bool ContainAsterFlg)
        {


            return 1;
        }
        private static int IsStrait(List<int> handsNumber, bool ContainAsterFlg)
        {
            if (handsNumber[handsNumber.Count - 1] - handsNumber[0]  == 4)
            {

            }
           

            return 1;
        }


        private static int IsTwoPair(List<int> handsNumber, bool ContainAsterFlg)
        {
            //重複している要素を削って残ったリストの数を返す
            var IsNumberMatch = handsNumber.Distinct<int>().Count<int>();


            if (ContainAsterFlg == true && IsNumberMatch == 5 ||
                ContainAsterFlg == false && IsNumberMatch == 4)
            {
                Console.WriteLine("Your Role Is OnePair");
            }
            return 1;
        }



        private static int IsOnePair(List<int> handsNumber, bool ContainAsterFlg)
        {
            //重複している要素を削って残ったリストの数を返す
            var IsNumberMatch = handsNumber.Distinct<int>().Count<int>();
            if (ContainAsterFlg == true &&  IsNumberMatch == 5　||
                ContainAsterFlg == false && IsNumberMatch == 4)
            {
                Console.WriteLine("Your Role Is OnePair");
            }
            return 1;
        }





        private static int IsFlush(List<int> handsID, List<int> handsMark, List<int> handsNumber, bool ContainAsterFlg)
        {
            //フラッシュの判定
            //アスタリスクを含むかチェック

            //listに要素をaddしていき
            //distinct()で重複要素を削除し
            //string.Join("", ~~)した文字列の長さが指定数以下

            List<int> markerList = new List<int>();
            //bool IsFlushContainJoker = false;
            var IsMarkAllMatch = handsMark.Distinct<int>().Count<int>();
            string handsMarkString = string.Join("", handsMark);
            int MaxCount = 0;

            //Console.WriteLine("string.Join " + string.Join("", handsMark).Length);
            Console.WriteLine("string.Join " + handsMarkString);
            //フラッシュの要件でジョーカーがある場合を判定
            if (ContainAsterFlg == true && IsMarkAllMatch == 2)
            {
                var duplicateMark = ListUtils.FindDuplication(handsMark);
                

                for (int i = 0; i < duplicateMark.Count; i++)
                {
                    int distinctCount = (from x in handsMark select x).Distinct().Count();
                    int d = (handsMarkString.Length -
                                handsMarkString.Replace(duplicateMark[i].ToString(), "").Length);

                    if (MaxCount > d) MaxCount = d;
                }
            }


            if ((ContainAsterFlg == false && IsMarkAllMatch == 1) ||
                (ContainAsterFlg == true && MaxCount == 4 ))
            {
                Console.WriteLine("Your Role Is Flush");
            }


            


            
            
            return 1;
        }

        private static int IsNoPair(List<int> handsID, List<int> handsMark, List<int> handsNumber, bool ContainAsterFlg)
        {
            var IsMarkAllMatch = handsMark.Distinct<int>().Count<int>();

            if (ContainAsterFlg == false && IsMarkAllMatch == 5)
            {
                Console.WriteLine("Your Role Is NoPair");
            }
            return 1;
        }







        //重複している要素が最大値で幾つなのかをカウントして返すメソッド
        private static List<Dictionary<string, int>> CheckMaxDuplicate(List<int> handsFactor)
        { 
            //List<T> から配列へ変換する - C#プログラミング
            //https://www.ipentec.com/document/csharp-convert-generics-list-to-array

            //マークもナンバーもあり得るので引数に取っている変数名は要素ということでFactorにしている
            int resultMaxDuplicate = 0;
            List<Dictionary<string,int>> MaxList = new List<Dictionary<string, int>>
            {            };

            int count = 0;

            //一旦リストを配列に変える
            var line = handsFactor.ToArray();
            //重複要素を削除
            var uniqArr = line.Distinct().ToArray();
            //要素別にカウント
            foreach (var x in uniqArr)
            {
                int MaxP = line.Count(a => a == x);

                if (MaxP >= 2)
                {
                    //if (count == 0)
                    //{
                    //    resultMaxDuplicate = MaxP;
                    //}
                    //else
                    //{
                    //    if (resultMaxDuplicate < MaxP)
                    //    {
                    //        resultMaxDuplicate = MaxP;
                    //    }
                    //}


                    //resultMaxDuplicate = MaxP;
                    MaxList.Add(new Dictionary<string, int>() { { x.ToString(), MaxP } });

                    count++;
                }
            }



            //for (int i = 0; i < duplicateMark.Count; i++)
            //{
            //    int distinctCount = (from x in handsFactor select x).Distinct().Count();
            //    //int d = (handsMarkString.Length -
            //    //            handsMarkString.Replace(duplicateMark[i].ToString(), "").Length);

            //    if (resultMaxDuplicate > d) resultMaxDuplicate = d;
            //}


            return MaxList;
        }

    }


    //【C#】List中の重複する要素を抽出する方法
    //https://qiita.com/nkojima/items/c927255b8d621d714f0a
    public class ListUtils
    {
        /// <summary>
        /// 引数のリスト（何らかの名称のリスト）から、重複する要素を抽出する。
        /// </summary>
        /// <param name="list">何らかの名称のリスト。</param>
        /// <returns>重複している要素のリスト。</returns>
        public static List<int> FindDuplication(List<int> list)
        {
            // 要素名でGroupByした後、グループ内の件数が2以上（※重複あり）に絞り込み、
            // 最後にIGrouping.Keyからグループ化に使ったキーを抽出している。
            var duplicates = list.GroupBy(name => name).Where(name => name.Count() > 1)
                .Select(group => group.Key).ToList();

            return duplicates;
        }
    }
}
