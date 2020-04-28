using System;

namespace EncryptDecrypt
{
    public class Util
    {
        public static string GetPlainText()
        {
            Console.Write("Please enter your message and press Enter: ");
            return Console.ReadLine();
            
        }

        public static string GetSingleKey()
        {
            
            Console.Write("Please enter your single key and press Enter: ");
            return Console.ReadLine();
        }

        public static string GetMultiKey()
        {
            
            Console.Write("Please enter your multi key and press Enter: ");
            return Console.ReadLine();
        }

        public static int[] Clean(string plain_text)
        {
            
            string cleanString = plain_text.ToUpper().Replace(" ", String.Empty);
            int length = cleanString.Length;
            int[] asciiOutput = new int[length];

            char[] stringArray = cleanString.ToCharArray();

            for (int i = 0; i < length; i++)
            {
                asciiOutput[i] = (int)stringArray[i];
            }
            

            return asciiOutput;
        }

        public static string SingleEnc(int[] clean_text, int[] clean_skey)
        {
            int single_key = clean_skey[0]-64;
            int length = clean_text.Length;
            char[] stringArray = new char[length];
            for (int i = 0; i < length; i++)
            {
                if (clean_text[i] + single_key > 90)
                {
                    clean_text[i] = 64 + ((clean_text[i] + single_key) - 90);
                }
                else
                {
                    clean_text[i] += single_key;
                }

                stringArray[i] = (char)clean_text[i];
            }

            string output = new string(stringArray);

            return output;
            
        }

        public static string MultiEnc(int[] clean_text, int[] clean_mkey)
        {
            int messageLength = clean_text.Length;
            int keyLength = clean_mkey.Length;
            char[] stringArray = new char[messageLength];

            for (int i = 0, j = 0; i < messageLength; i++, j++)
            {
                if (j == keyLength )
                {
                    j = 0;
                }

                int key = clean_mkey[j] - 64;
               
                if (clean_text[i] + (key) > 90)
                {
                    clean_text[i] = 64 + ((clean_text[i] + key) - 90);
                }
                else
                {
                    clean_text[i] += key;
                }

                stringArray[i] = (char)clean_text[i];
               

            }

            string output = new string(stringArray);


            return output;
        }

        public static string ContiEnc(int[] clean_text, int[] clean_mkey)
        {
            int messageLength = clean_text.Length;
            int keyLength = clean_mkey.Length;
            int[] newKey = new int[messageLength];
            char[] stringArray = new char[messageLength];

            for (int i = 0; i < messageLength; i++)
            {
                if (i < keyLength)
                {
                    newKey[i] = clean_mkey[i] - 64;
                }
                else
                {
                    newKey[i] = clean_text[i - keyLength] - 64;
                }
            }

            for (int i = 0; i < messageLength; i++)
            {
                if (clean_text[i] + newKey[i] > 90)
                {
                    clean_text[i] = 64 + ((clean_text[i] + newKey[i]) - 90);
                }
                else
                {
                    clean_text[i] += newKey[i];
                }

                stringArray[i] = (char)clean_text[i];
            }

            string output = new string(stringArray);

            return output;



        }

        public static string SingleDec(string enc_single, int[] clean_skey)
        {
            int single_key = clean_skey[0] - 64;
            int length = enc_single.Length;
            int[] asciiOutput = new int[length];
            char[] stringArray = enc_single.ToCharArray();

            for (int i = 0; i < length; i++)
            {
                if (((int)stringArray[i]) - single_key < 65)
                {
                    asciiOutput[i] = 91 - (65 - ((int)stringArray[i]) - single_key);
                }
                else
                {
                    asciiOutput[i] = ((int)stringArray[i]) - single_key;
                }

                stringArray[i] = (char)asciiOutput[i];

            }

            string output = new string(stringArray);

            return output;



        }

        public static string MultiDec(string enc_multi, int[] clean_mkey)
        {
            int messageLength = enc_multi.Length;
            int keyLength = clean_mkey.Length;
            int[] asciiOutput = new int[messageLength];
            char[] stringArray = enc_multi.ToCharArray();

            for (int i = 0, j = 0; i < messageLength; i++, j++)
            {
                if (j == keyLength)
                {
                    j = 0;
                }

                int key = clean_mkey[j] - 64;

                if (((int)stringArray[i]) - key < 65)
                {
                    asciiOutput[i] = 91 - (65 - ((int)stringArray[i]) - key);
                }
                else
                {
                    asciiOutput[i] = ((int)stringArray[i]) - key;
                }

                stringArray[i] = (char)asciiOutput[i];

            }

            string output = new string(stringArray);

            return output;
        }

        public static string ContiDec(string enc_conti, int[] clean_mkey)
        {
            int messageLength = enc_conti.Length;
            int keyLength = clean_mkey.Length;
            int[] newKey = new int[messageLength];
            char[] stringArray = enc_conti.ToCharArray();

            for (int i = 0; i < messageLength; i++)
            {
                if (i < keyLength)
                {
                    newKey[i] = clean_mkey[i] - 64;
                }
                else
                {
                    newKey[i] = enc_conti[i - keyLength] - 64;
                }
            }




            return "hello";

        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            
            string plain_text = Util.GetPlainText();
            string single_key = Util.GetSingleKey();
            string multi_key = Util.GetMultiKey();
            Console.WriteLine();

            Console.WriteLine($"You entered [{plain_text}] as plain text");
            Console.WriteLine($"You entered [{single_key}] as your single key");
            Console.WriteLine($"You entered [{multi_key}] as your multi key");
            Console.WriteLine();

            int[] clean_text = Util.Clean(plain_text);
            int[] clean_text1 = Util.Clean(plain_text);
            int[] clean_text2 = Util.Clean(plain_text);
            int[] clean_skey = Util.Clean(single_key);
            int[] clean_mkey = Util.Clean(multi_key);
            
        

            string enc_single = Util.SingleEnc(clean_text, clean_skey);
            string enc_multi = Util.MultiEnc(clean_text1, clean_mkey);
            string enc_conti = Util.ContiEnc(clean_text2, clean_mkey);

            Console.WriteLine($"Encrypted message with single key is [{enc_single}]");
            Console.WriteLine($"Encrypted message with multi key is [{enc_multi}]");
            Console.WriteLine($"Encrypted message with continuous key is [{enc_conti}]");
            Console.WriteLine();

            string dec_single = Util.SingleDec(enc_single, clean_skey);
            string dec_multi = Util.MultiDec(enc_multi, clean_mkey);
            //string dec_conti = Util.ContiDec(enc_conti, clean_mkey);

            Console.WriteLine($"Decrypted message with single key is [{dec_single}]");
            Console.WriteLine($"Decrypted message with multi key is [{dec_multi}]");
            //Console.WriteLine($"Decrypted message with continuous key is [{dec_conti}]");
            Console.WriteLine();

            
        }
    }
}
