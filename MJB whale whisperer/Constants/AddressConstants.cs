using System;
using System.Collections.Generic;
using System.Text;

namespace MJB_whale_whisperer.Constants
{
    public class AddressConstants
    {
        /// <summary>
        /// https://medium.com/@btcWolves/bch-heading-towards-the-abyss-f1b2b9ec41f1
        /// </summary>
        public static string[] BITCOIN_CASH_ADDRESSES =
        {
            "19hZx234vNtLazfx5J2bxHsiWEmeYE8a7k", // Bitmain’s BCH Sales Wallet 
            "1BgatB78WrFLdCgnPnBqiDcNFFA46jkPZe", // “Unknown BCH Mining Wallet #1”
            "17Wk4GPKw9nZ9PbspzaxN3fv1L2m9NA9dg", // Unknown BCH Mining Wallet #2
            "qrhmqzxvasfdqcjgkvan3huky2gxw0u0xun0yqq9ut" // Unknown wallet
        };

        public static string[] BITMAIN_BITCOIN_ADDRESSES =
        {
            "16rCmCmbuWDhPjWTrpQGaU3EPdZF7MTdUk", // https://pastebin.com/5Yp6kcQe
            //"13vHWR3iLsHeYwT42RnuKYNBoVPrKKZgRv", // mining address
        };

        /// <summary>
        /// http://gaelb.alwaysdata.net/MTgox_watch_CW/index.html
        /// </summary>
        public static string[] MTGOX_TRUSTEE_ADDRESSES = {
            "12KkeeRkiNS13GMbg7zos9KRn9ggvZtZgx",
            "12T4oSNd4t9ty9fodgNd47TWhK35pAxDYN",
            "13ahgw8sM95EDbugT3tdb8TYoMU46Uw7PX",
            "13dXFMyG22EsUsvaWhCqUo7SXuX7rBPog6",
            "13sXfpp2V16nnxYvW9FHHoBdMa3k98uJw8",
            "13Wv5hGhubAWgSPWtXYh6s1s7HX2N1psYg",
            "13xGCc4TPSYY9GYxBGVNox82KxyjkFnxMX",
            "1439q4Na8v88kPBqoyg8F4ueL9SYr8ANWj",
            "14mP6caC5dFhHdVAPCjPKM8Nm36MBDR5pM",
            "14p4w3TRCd6NMRSnzTmgdvQhNnbrAmzXmy",
            "14USZ558Rr28AZwdJQyciSQkN4JT1cEoj2",
            "155FsTtEFq4eGCcBxDseuwLKPbmtWbyHJR",
            "156HpsWfgkWYLT63uhTAGUSUF3ZMnB9WWj",
            "15kNZcrhxeFZgVVLK2Yjzd69tRidbFdJEZ",
            "15QcKCa84ZCHxbsqXDoKhi5XbmQB8jPEAd",
            "15SeCwVCFx5cWyrcdD1Zp1D1zxjH2SELPg",
            "15U4VsmWG1cdXAtizvQsW4r7iMxzp64Tgu",
            "16jZZkMYqjUWUtQ9DfDvHdH5ko5BcnH9XQ",
            "16W4XcUAKPmSES9MiUCio28msSCp8rDZgs",
            "16w6sZBDP58yyeyZAcvnxcEGJpwR9amM6g",
            "17etv2L3nhk6SCcWSNW4eoZkBy84izAm17",
            "17KcBp8g76Ue8pywgjta4q8Ds6wK4bEKp7",
            "17Tf4bVQaCzwWrDWGRPC97RLCHnU4LY8Qr",
            "18hcZVFPqDNAovJmb9vA6hEJrDz6uWXNG",
            "18KDS3q6a4YV9Nn8jcyMvNoVPfcrfemeag",
            "18M1Z337NqLtK9V69bssnQUYsvb7hmfSFS",
            "18ok25NTkdrUzdByFJCNVsqVYkujZ8aP45",
            "18YDgRhxsomuBZ1g9d8Y1JuRmxDhF8Bvff",
            "195HvmjXgoF3M5vFaBC8swZPhwrE7VhxRD",
            "199Yxz2TJGtND3QKsHTptTJivqSaUZBvku",
            "19c8sUa54yQuRTVDfJa3iDkkCaFkzBJLPB",
            "19Cr4zXpKw43xLJhFZW9iv4DDNtQk2TDeB",
            "19eihBKk6e5YD2QXAe4SVUsxRLLnTDKsfv",
            "19KiFrafXEyJCUDYFEv3B6tBUwyfFo7kNU",
            "1Ar6meJQCkNoC9wnPcyRNNpzX5fBDaGcKd",
            "1AZu7TQmKBAes2duNDctYwjAB9nhHczUnA",
            "1B6kJM75iu5ty1HAHMMz6tT1HhjoGNTCa9",
            "1BDZBTb4KE5oq6wAgA6EvAe3uCFRrAbPao",
            "1BXyJc6BVuTFnHQCcjiWX2xmCPNVfaSZeb",
            "1BzK87zuqidZn489Wb2oLSktrjKrX7TLKe",
            "1C5aU4Xnpd3txbxehk46UZgiuNB8QdpHCH",
            "1CRjKZJu8LvTutnSKq4zTJ4yiqrzMAArYW",
            "1CZsoJfkknbnW5fKrt1oR7N1ALE5WmDGP1",
            "1DedUxzgwErg4ipNi988wPgLk5thwciKcc",
            "1Drshi4RAuvxk4T6Bkq959ZvLbvy7b1wvD",
            "1EiiKCCnFgHjEvPZdu29qqgdBm8zTvpU3U",
            "1EK8vW7UYaYHKiW4TZmYJKtwcZLM14VjvP",
            "1FhRuUkk8Bfx8FJDemtxhKAR4F8GCNKrXG",
            "1FrV9hv1AW34BGJvobJatyzUWYDWB9epRW",
            "1Fu4YgM3Y9CxvioGPqkSzkydAC8MVaPN1D",
            "1G23Uzwj55k2A9TRwaTknqGav66oDTkWCu",
            "1GkZQcDy8V6pmHFZqUBUBCnN9dc2hoWasD",
            "1GyDutntMuYyA2vQGW5HFcKLfx4cbDdbJq",
            "1H4K3dGfNbAN4AUfyUrpkGpjrd83sntDpV",
            "1Hb8DmmvvtTYv5RBLuGtDxznkZwVpd5Vjy",
            "1HdKXsNQtzDcfB6PGM7DWTgX9vhBWsz1ak",
            "1Hm6XDmhKCHz68wDEYTapN9MEanke8iwUk",
            "1HuPVqz2xvf1rdNFUqd62vRTyxP3jeX9Ch",
            "1HweN9p41BY2RBunsPqyVuheEq7gVoxA9u",
            "1HX4s3JeFU3x1eQgPNQVAdx6FoCtbb1hr8",
            "1HzEPuenagLEWj68igDXBBXrzc293RuR5V",
            "1JtgU6Uo1RAt5eiMf34EehyatUezBQP36C",
            "1JVmoJT3471FjsX5H4hAeR1RyrDgpkHbpm",
            "1JVU43LNKXqa9W5fCh8tppxDDEWgfeNg46",
            "1JztCg7eKSkb1vi7NzGJynXpLZmoaFtYud",
            "1KFDUSZuapMv7YaDmL6cyrHTQhma1MtFYs",
            "1LLc8aA9C9LLULGbYCYSFKXgxKP2DXdCqP",
            "1LS5EFRRMDgMQusW6zokQUHjzNUfy6HHCQ",
            "1LueUjEuBgc7cQhsWT8zAfTjcWmrNBZXaR",
            "1LXi3x7hyt17cxncscGE887WCrC6XDNZ4P",
            "1LzwbLgdKd4eFLkpRdeajkH1YJkVCip2zj",
            "1MkyfwJf7uhWTmVGGQXfcT5ip31DoHMxsz",
            "1Mm9brripN4RPTzkGnRrbt5uDWdqbfk2iX",
            "1MPJJzRaT8vLhowNB4dVyWRxxu79dq7WkB",
            "1MvpYtqgBH7CXbTutrSVCTNHPzm9vakuRy",
            "1N5X4kcZ56uRh24XrZoztS9Vb8G7j1Joop",
            "1NA3Tj4b1jtx9eGELe31Jw4DrzTqKP3ayH",
            "1Pq7hooZbEAz5y3QMnqFY8C5xqTdrjUwcA",
            "1PRXQEoL8vzEzoJJ9hbtAP6NaV2daccAUn",
            "1PxGTuJzDx1ceFHx4Z5CHaWuhiPBNovmZD" };
    }
}
