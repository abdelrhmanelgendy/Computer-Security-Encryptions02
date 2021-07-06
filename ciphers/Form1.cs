using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ciphers
{
    public partial class Form1 : Form
    {
        string textToIncryption;
        string Key;

        public Form1()
        {
            InitializeComponent();
            comboBox1.Text = "Ceaser";
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {

            
            textToIncryption = txttoencrypt.Text;
            Key = txtKey.Text;
            switch (comboBox1.Text)
            {

                case "Ceaser":
                    if (!keyRequired())
                    {
                        return;
                    }
                    panel1.Visible = true;
                    textToIncryption = txttoencrypt.Text;
                    Key = txtKey.Text;
                    string cipherText = Ceaser.Encipher(textToIncryption, Convert.ToInt32(Key));
                    txtEncrypted.Text = cipherText;
                    txtdecrypty.Text = Ceaser.Decipher(cipherText, Convert.ToInt32(Key));
                    break;

                case "Monoalphaptic":
                    
                    panel1.Visible = false;
                    string key = "zyxwvutsrqponmlkjihgfedcbaABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    string MonoCipher =Monoalphaptic.Encrypt(textToIncryption, key);
                    txtEncrypted.Text = MonoCipher;
                    txtdecrypty.Text = Monoalphaptic.Decrypt(MonoCipher,key);
                    panel1.Visible = false;
                    break;

                case "Playfair":
                    if (!keyRequired())
                    {
                        return;
                    }
                    panel1.Visible = true;


                    txtEncrypted.Text = playfair.Encipher(txttoencrypt.Text, txtKey.Text);
                    txtdecrypty.Text = playfair.Decipher(playfair.Encipher(txttoencrypt.Text, txtKey.Text), txtKey.Text);
                  
                    break;
                case "Viegener":
                    if (!keyRequired())
                    {
                        return;
                    }
                    panel1.Visible = true;
                   
                   
                    string VCipher = Vigener.Encipher(textToIncryption, Key);
                    txtEncrypted.Text = VCipher;
                    txtdecrypty.Text = Vigener.Decipher(VCipher, Key);
                    break;
                case "Railfence":
                    if (!keyRequired())
                    {
                        return;
                    }
                    panel1.Visible = true;
                    
                    string RCipher = railFence.Encrypt(textToIncryption, Convert.ToInt32(Key));
                    txtEncrypted.Text = RCipher;
                    txtdecrypty.Text = railFence.Decrypt(RCipher,Convert.ToInt32(Key));
                    break;

                case "Polyalphaptic":
                    if (!keyRequired())
                    {
                        return;
                    }
                    panel1.Visible = true;


                    string PCipher = new PolyAlphapetic().Encrypt(textToIncryption, Key);
                    txtEncrypted.Text = PCipher;
                    txtdecrypty.Text = new PolyAlphapetic().Decrypt(PCipher, Key);
                    break;
            }
                //if (bunifuTextBox3.Text=="")
                //{

                //    System.Media.SystemSounds.Beep.Play();
                //    return;
                //}
                //label1.Text = Key;

            }
            catch (Exception)
            {

                throw;
            }
        }

        static class Ceaser
        {
            public static char cipher(char ch, int key)
            {
                if (!char.IsLetter(ch))
                {

                    return ch;
                }

                char d = char.IsUpper(ch) ? 'A' : 'a';
                return (char)((((ch + key) - d) % 26) + d);


            }


            public static string Encipher(string input, int key)
            {
                string output = string.Empty;

                foreach (char ch in input)
                    output += cipher(ch, key);

                return output;
            }

            public static string Decipher(string input, int key)
            {
                return Encipher(input, 26 - key);
            }

        }
      public  static class Monoalphaptic
        {
        public    static string Encrypt(string plainText, string keyy)
            {
                char[] chars = new char[plainText.Length];
                for (int i = 0; i < plainText.Length; i++)
                {
                    if (plainText[i] == ' ')
                    {
                        chars[i] = ' ';
                    }

                    else
                    {
                        int j = plainText[i] - 97;
                        chars[i] = keyy[j];
                    }
                }

                return new string(chars);
            }


            public static string reverse(string cipherText)
            {
                char[] charArray = cipherText.ToCharArray();
                Array.Reverse(charArray);

                return new string(charArray);
            }


       public     static string Decrypt(string cipherText, string key)
            {
                char[] chars = new char[cipherText.Length];
                for (int i = 0; i < cipherText.Length; i++)
                {
                    if (cipherText[i] == ' ')
                    {
                        chars[i] = ' ';
                    }
                    else
                    {
                        int j = key.IndexOf(cipherText[i]) + 97;
                        chars[i] = (char)j;
                    }
                }
                return new string(chars);
            }
        }


        static class playfair {

            private static int Mod(int a, int b)
            {
                return (a % b + b) % b;
            }

            private static List<int> FindAllOccurrences(string str, char value)
            {
                List<int> indexes = new List<int>();

                int index = 0;
                while ((index = str.IndexOf(value, index)) != -1)
                    indexes.Add(index++);

                return indexes;
            }

            private static string RemoveAllDuplicates(string str, List<int> indexes)
            {
                string retVal = str;

                for (int i = indexes.Count - 1; i >= 1; i--)
                    retVal = retVal.Remove(indexes[i], 1);

                return retVal;
            }

            private static char[,] GenerateKeySquare(string key)
            {
                char[,] keySquare = new char[5, 5];
                string defaultKeySquare = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
                string tempKey = string.IsNullOrEmpty(key) ? "CIPHER" : key.ToUpper();

                tempKey = tempKey.Replace("J", "");
                tempKey += defaultKeySquare;

                for (int i = 0; i < 25; ++i)
                {
                    List<int> indexes = FindAllOccurrences(tempKey, defaultKeySquare[i]);
                    tempKey = RemoveAllDuplicates(tempKey, indexes);
                }

                tempKey = tempKey.Substring(0, 25);

                for (int i = 0; i < 25; ++i)
                    keySquare[(i / 5), (i % 5)] = tempKey[i];

                return keySquare;
            }

            private static void GetPosition(ref char[,] keySquare, char ch, ref int row, ref int col)
            {
                if (ch == 'J')
                    GetPosition(ref keySquare, 'I', ref row, ref col);

                for (int i = 0; i < 5; ++i)
                    for (int j = 0; j < 5; ++j)
                        if (keySquare[i, j] == ch)
                        {
                            row = i;
                            col = j;
                        }
            }

            private static char[] SameRow(ref char[,] keySquare, int row, int col1, int col2, int encipher)
            {
                return new char[] { keySquare[row, Mod((col1 + encipher), 5)], keySquare[row, Mod((col2 + encipher), 5)] };
            }

            private static char[] SameColumn(ref char[,] keySquare, int col, int row1, int row2, int encipher)
            {
                return new char[] { keySquare[Mod((row1 + encipher), 5), col], keySquare[Mod((row2 + encipher), 5), col] };
            }

            private static char[] SameRowColumn(ref char[,] keySquare, int row, int col, int encipher)
            {
                return new char[] { keySquare[Mod((row + encipher), 5), Mod((col + encipher), 5)], keySquare[Mod((row + encipher), 5), Mod((col + encipher), 5)] };
            }

            private static char[] DifferentRowColumn(ref char[,] keySquare, int row1, int col1, int row2, int col2)
            {
                return new char[] { keySquare[row1, col2], keySquare[row2, col1] };
            }

            private static string RemoveOtherChars(string input)
            {
                string output = input;

                for (int i = 0; i < output.Length; ++i)
                    if (!char.IsLetter(output[i]))
                        output = output.Remove(i, 1);

                return output;
            }

            private static string AdjustOutput(string input, string output)
            {
                StringBuilder retVal = new StringBuilder(output);

                for (int i = 0; i < input.Length; ++i)
                {
                    if (!char.IsLetter(input[i]))
                        retVal = retVal.Insert(i, input[i].ToString());

                    if (char.IsLower(input[i]))
                        retVal[i] = char.ToLower(retVal[i]);
                }

                return retVal.ToString();
            }

            private static string Cipher(string input, string key, bool encipher)
            {
                string retVal = string.Empty;
                char[,] keySquare = GenerateKeySquare(key);
                string tempInput = RemoveOtherChars(input);
                int e = encipher ? 1 : -1;

                if ((tempInput.Length % 2) != 0)
                    tempInput += "X";

                for (int i = 0; i < tempInput.Length; i += 2)
                {
                    int row1 = 0;
                    int col1 = 0;
                    int row2 = 0;
                    int col2 = 0;

                    GetPosition(ref keySquare, char.ToUpper(tempInput[i]), ref row1, ref col1);
                    GetPosition(ref keySquare, char.ToUpper(tempInput[i + 1]), ref row2, ref col2);

                    if (row1 == row2 && col1 == col2)
                    {
                        retVal += new string(SameRowColumn(ref keySquare, row1, col1, e));
                    }
                    else if (row1 == row2)
                    {
                        retVal += new string(SameRow(ref keySquare, row1, col1, col2, e));
                    }
                    else if (col1 == col2)
                    {
                        retVal += new string(SameColumn(ref keySquare, col1, row1, row2, e));
                    }
                    else
                    {
                        retVal += new string(DifferentRowColumn(ref keySquare, row1, col1, row2, col2));
                    }
                }

                retVal = AdjustOutput(input, retVal);

                return retVal;
            }

            public static string Encipher(string input, string key)
            {
                return Cipher(input, key, true);
            }

            public static string Decipher(string input, string key)
            {
                return Cipher(input, key, false);
            }
        }
        static class Vigener
        {
            private static int Mod(int a, int b)
            {
                return (a % b + b) % b;
            }

            private static string Cipher(string input, string key, bool encipher)
            {
                for (int i = 0; i < key.Length; ++i)
                    if (!char.IsLetter(key[i]))
                        return null; // Error

                string output = string.Empty;
                int nonAlphaCharCount = 0;

                for (int i = 0; i < input.Length; ++i)
                {
                    if (char.IsLetter(input[i]))
                    {
                        bool cIsUpper = char.IsUpper(input[i]);
                        char offset = cIsUpper ? 'A' : 'a';
                        int keyIndex = (i - nonAlphaCharCount) % key.Length;
                        int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
                        k = encipher ? k : -k;
                        char ch = (char)((Mod(((input[i] + k) - offset), 26)) + offset);
                        output += ch;
                    }
                    else
                    {
                        output += input[i];
                        ++nonAlphaCharCount;
                    }
                }

                return output;
            }

            public static string Encipher(string input, string key)
            {
                return Cipher(input, key, true);
            }

            public static string Decipher(string input, string key)
            {
                return Cipher(input, key, false);
            }
        }
        static class railFence
        {
            public static string Encrypt(string text, int rails)
            {

                text = text.ToUpper();
                text = Regex.Replace(text, @"[^A-Z0-9]", string.Empty);

                var lines = new List<StringBuilder>();

                for (int i = 0; i < rails; i++)
                    lines.Add(new StringBuilder());

                int currentLine = 0;
                int direction = 1;

                for (int i = 0; i < text.Length; i++)
                {
                    lines[currentLine].Append(text[i]);

                    if (currentLine == 0)
                        direction = 1;
                    else if (currentLine == rails - 1)
                        direction = -1;

                    currentLine += direction;
                }


                StringBuilder result = new StringBuilder();

                for (int i = 0; i < rails; i++)
                    result.Append(lines[i].ToString());

                return result.ToString();
            }


            public static string Decrypt(string text, int rails)
            {

                text = text.ToUpper();
                text = Regex.Replace(text, @"[^A-Z0-9]", string.Empty);

                var lines = new List<StringBuilder>();

                for (int i = 0; i < rails; i++)
                    lines.Add(new StringBuilder());


                int[] linesLenght = Enumerable.Repeat(0, rails).ToArray();


                int currentLine = 0;
                int direction = 1;

                for (int i = 0; i < text.Length; i++)
                {
                    linesLenght[currentLine]++;

                    if (currentLine == 0)
                        direction = 1;
                    else if (currentLine == rails - 1)
                        direction = -1;

                    currentLine += direction;
                }

                int currentChar = 0;

                for (int line = 0; line < rails; line++)
                {
                    for (int c = 0; c < linesLenght[line]; c++)
                    {
                        lines[line].Append(text[currentChar]);
                        currentChar++;
                    }
                }

                StringBuilder result = new StringBuilder();

                currentLine = 0;
                direction = 1;


                int[] currentReadLine = Enumerable.Repeat(0, rails).ToArray();

                for (int i = 0; i < text.Length; i++)
                {

                    result.Append(lines[currentLine][currentReadLine[currentLine]]);
                    currentReadLine[currentLine]++;

                    if (currentLine == 0)
                        direction = 1;
                    else if (currentLine == rails - 1)
                        direction = -1;

                    currentLine += direction;
                }

                return result.ToString();

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblKeyRq.Visible = false;
            txtKey.Text = "";
            txtdecrypty.Text = "";
            txtEncrypted.Text = "";
            switch (comboBox1.Text)
            { 
              case "Ceaser":
                    Chars = true;
                    text = false;
                    panel1.Visible = true;
                    label1.Text = "Key";
          
            break;
                  
                case "Monoalphaptic":
                    txttoencrypt.Text = "";
                    panel1.Visible = false;
          
            break;
                   
                case "Playfair":
                        Chars = false;
                        text = false;
                    
                    panel1.Visible = true;

            break;
                case "Viegener":
                    panel1.Visible = true;
                    txtKey.Text = "";
                    text = true;
                    Chars = false;


                    break;
                case "Railfence":
                    panel1.Visible = true;
                    txtKey.Text = "";
                    Chars = true;
                    text = false;
                    break;
                case "Polyalphaptic":
                    
                    
                    
                    
                    Chars = false;
                    text = false;

                    panel1.Visible = true;
                    

                    break;

            }
    }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Boolean Chars = false;
        Boolean text = false;

        private void txtKey_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (Chars)
              {
                  if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                  {
                      e.Handled = true;
                  }
              }
            if (text)
            {
                if (!char.IsLetter(e.KeyChar) && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
         

         
        }

        bool keyRequired()
        {
            if (txtKey.Text=="")
            {
                lblKeyRq.Visible = true;
                System.Media.SystemSounds.Beep.Play();
                return false;
            }
            else
            {
                lblKeyRq.Visible = false;
                return true;
            }

        }

        private void txttoencrypt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttoencrypt_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
    /*class MonoAlphapetich {

        Alphabet alph = new Alphabet();

            public string Encrypt(string sourcetext, string key)
            {
                StringBuilder code = new StringBuilder();
                int[] key_id = new int[key.Length];
                int t = 0;

                //поиск индексов букв ключа
                for (int i = 0; i < key.Length; i++)
                {
                    for (int j = 0; j < alph.lang.Length; j++)
                    {
                        if (key[i] == alph.lang[j])
                        {
                            key_id[i] = j;
                            break;
                        }
                    }
                }

                for (int i = 0; i < sourcetext.Length; i++)
                {
                    //поиск символа в алфавите
                    for (int j = 0; j < alph.lang.Length; j++)
                    {
                        //если символ найден
                        if (sourcetext[i] == alph.lang[j])
                        {
                            if (t > key.Length - 1)
                            {
                                t = 0;
                            }
                            code.Append(alph.lang[(j + key_id[t]) % alph.lang.Length]);
                            t++;

                            break;
                        }
                        //если символ не найден
                        else if (j == alph.lang.Length - 1)
                        {
                            code.Append(sourcetext[i]);
                            t++;
                        }
                    }
                }

                return code.ToString();
            }

            public string Decrypt(string sourcetext, string key)
            {
                StringBuilder code = new StringBuilder();
                int[] key_id = new int[key.Length];
                int t = 0;

                //поиск индексов букв ключа
                for (int i = 0; i < key.Length; i++)
                {
                    for (int j = 0; j < alph.lang.Length; j++)
                    {
                        if (key[i] == alph.lang[j])
                        {
                            key_id[i] = j;
                            break;
                        }
                    }
                }

                for (int i = 0; i < sourcetext.Length; i++)
                {
                    //поиск символа в алфавите
                    for (int j = 0; j < alph.lang.Length; j++)
                    {
                        //если символ найден
                        if (sourcetext[i] == alph.lang[j])
                        {
                            if (t > key.Length - 1)
                            {
                                t = 0;
                            }
                            code.Append(alph.lang[(j + alph.lang.Length - key_id[t]) % alph.lang.Length]);
                            t++;
                            break;
                        }
                        //если символ не найден
                        else if (j == alph.lang.Length - 1)
                        {
                            code.Append(sourcetext[i]);
                            t++;
                        }
                    }
                }

                return code.ToString();
            }
        }
    */
    //}
}
