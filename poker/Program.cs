using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;

//Marker型なので、stringやintにする場合には型変換が必要
//https://www.sejuku.net/blog/50282
enum Marker
{
    C = 0,  //club
    D,      //diamond
    H,      //heart
    S       //spade

    //スペードが一番強い
}

enum Poker_role : byte
{
    NoPair = 0,
    OnePair,
    TwoPair,
    ThreeCard,
    Strait,
    flush,
    FullHouse,
    FourCard,
    StraitFlush,
    RoyalStraitFlush,
    fiveCard
    //ロイヤルストレートフラッシュが一番強い
}


namespace poker
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://www.webclap-dandy.com/?category=Programing&id=11


            int[,] handsCardArray = new int[5,3];
            //デッキから既に出たカードかどうかを判定用
            bool[,] deckBoolArray = new bool[4, 13];

            var cardSymbol = new string[4]
            {
                Marker.C.ToString(),
                Marker.D.ToString(),
                Marker.H.ToString(),
                Marker.S.ToString()
            };

            var baseCardsArray = new string[]
                { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

            //const int CARDLENGTH = 13;
            const int maxHandsCards = 5;
            //割った値が0なら
            //割った値のあまりがそのカードの種類のナンバーになる
            //但し、余りが0の場合はKになるので注意

            //ジョーカーは含んでいない
            //52枚

            //二重Forループ対策 (LINQ)
            //https://baba-s.hatenablog.com/entry/2020/01/10/090000
            // rowになるcardSymbolに対してcolmunのbaseCardsArrayを入れていく
            var cardsArrayEnumerable = cardSymbol.SelectMany(i1 => baseCardsArray.Select(i2 => (i1, i2)));

            //IEnumerableを配列に変換。Listへの型変換も可能
            //https://www.sejuku.net/blog/50282
            //https://pknight.hatenablog.com/entry/20130326/1364278225
            ValueTuple<string,string>[] allCardsArray = cardsArrayEnumerable.ToArray();
            //Console.WriteLine(allCardsArray.GetType());

            //ジョーカーの値を入れつのにまず配列長を1つ長くする
            Array.Resize(ref　allCardsArray, allCardsArray.Length + 1);
            //長くした配列の最後(53)にジョーカーの*をセット
            allCardsArray.SetValue(("*", "*"), allCardsArray.Length - 1);

            #region 古い書き方→上記のLINQ
            //上のLINQ処理は下記の処理と同じ
            //var CardsArray2 = new string[4,13];
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int k = 0; k < 13; k++)
            //    {
            //        CardsArray2[i, k] = baseCardsArray[k];
            //    }
            //}

            //{
            //    { "A", "2", "3", "4" ,"5", "6", "7", "8", "9", "10", "J", "Q", "K" },   //club
            //    { "A", "2", "3", "4" ,"5", "6", "7", "8", "9", "10", "J", "Q", "K" },   //diamond
            //    { "A", "2", "3", "4" ,"5", "6", "7", "8", "9", "10", "J", "Q", "K" },   //heart
            //    { "A", "2", "3", "4" ,"5", "6", "7", "8", "9", "10", "J", "Q", "K" }    //spade
            //};
            #endregion

            //tupleなので( , )で出力される
            //Console.WriteLine(allCardsArray[1]);

            //中身の要素の取り出し方
            //Console.WriteLine("{0}の{1}(番)", allCardsArray[22].Item1, allCardsArray[22].Item2);


            #region 別ファイルクラスのインスタンス生成か、型名の使用か
            //インスタンスを生成してそこからメソッド呼び出しはCS0176 error
            //https://docs.microsoft.com/ja-jp/dotnet/csharp/misc/cs0176
            DealCards gc = new DealCards();
            handsCardArray = gc.GetHandCards(handsCardArray);

            //こっちはDealCardsクラスのGetHandCardsメソッドが
            //public static int[] GetHandCards(int[] handsArray) だったら使用できる(静的メンバ参照)
            //handsArray = DealCards.GetHandCards(handsArray);

            //**********************************************************
            //問題は静的メンバからの参照とインスタンス生成はどっちがいいのか！？？？
            //**********************************************************
            #endregion 

            //for (int i = 1; i < 28; i++)
            //{
            //    divided = i / CARDLENGTH;
            //    remainder = i % CARDLENGTH;

            //    //Kingかどうか判定
            //    if (remainder == 0)
            //    {
            //        remainder = 13;
            //        divided --;
            //    }

            //    Console.WriteLine("i:{0} divided = {1},　remainder = {2}, card = {3} {4}",
            //        i,
            //        divided,
            //        remainder,
            //        cards[divided, remainder - 1],
            //        (Marker)divided
            //        );

            //}

            //allCardsArrayをvarで定義していた時に中身を取り出す際は
            //handsCardArray[i, 0] -1].i1でよかった
                    

            for (int i = 0; i < maxHandsCards; i++)
            {
                Console.Write($"{allCardsArray[handsCardArray[i, 0] - 1].Item1} ");
            }
            Console.WriteLine();

            for (int i = 0; i < maxHandsCards; i++)
            {
                Console.Write($"{allCardsArray[handsCardArray[i, 0] - 1].Item2} ");
            }

            Console.WriteLine();

            //Console.WriteLine((Poker_role)1);

            //for (int i = 0; i < handsArray.Length; i++)
            //{
            //    string[] role = (string[])AnyCards(line, line.Substring(i, 1));
            //    // Console.WriteLine(role[0] + " " + role[1] + " " + role[2]);
            //    // role[0]：重複数 intパース可
            //    // role[1]：調査文字

            //    //もしも重複カードがたくさんあった場合に変数更新
            //    //ck_Aは初期値が0なので、１回以上数値が代入される
            //    if (ck_A < int.Parse(role[0])) ck_A = int.Parse(role[0]);

            //    //もしもワンペアの場合
            //    if (int.Parse(role[0]) == 2)
            //    {
            //        //ck_A = int.Parse(role[0]);
            //        if (alph == "") alph = role[1]; //何の文字だったかを取得

            //        //ツーペアかどうかの判定
            //        // ワンペア目と文字が異なる場合
            //        if (role[1] != alph) ck_flg = true;
            //    }

            //    //もしもジョーカーを含む場合
            //    if (role[1] == "*") joker_flg = true;
            //}



            //Console.WriteLine("num = {0}, Poker_role)num = {1}", num, (Poker_role)num);

            //for (int i = 1; i < 14; ++i)
            //    Console.Write("{0}月  {1}\n", i, (Month)i);
           
            bool ContainAsterFlg = false;

            //アスタリスクを含むかチェック
            List<int> handsID = new List<int>();
            for (int i = 0; i < maxHandsCards; i++)
            {
                handsID.Add(handsCardArray[i, 0]);
            }
            ContainAsterFlg = handsID.Any(item => 0 == item.CompareTo(53));


            //string handsReplaceAster = data.replace("*", "");

            //ジョーカーが含まれている場合
            //ノーペア→ワンペア
            //ワンペア→スリーカード
            //ツーペア→フルハウス
            //スリーカード→フォーカード
            //フォーカード→ファイブカード

            //ノーペア→ワンペアよりストレート ＜ フラッシュ < ストレートフラッシュ

            int result = CheckRole.CheckHands(handsCardArray, ContainAsterFlg, baseCardsArray);

            Console.WriteLine((Poker_role)result);
        }
    }
}
