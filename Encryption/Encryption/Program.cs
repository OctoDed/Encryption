using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> eng_abc = new List<char>() {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', ' ' };
            //Аффинный шифр
            {
                Console.WriteLine("Аффинный шифр");
            N_in:
                Console.WriteLine("Введите n");
                int n = Convert.ToInt32(Console.ReadLine());
                if (n < 0)
                {
                    Console.WriteLine("Недопустимое значение n");
                    goto N_in;
                }
                Console.WriteLine("Введите a");
            Alpha_in:
                int alpha = Convert.ToInt32(Console.ReadLine());
                if (NOD(n, alpha) == false || alpha < 0)
                {
                    Console.WriteLine("Недопустимое значение a");
                    goto Alpha_in;
                }
            Beta_in:
                Console.WriteLine("Введите b");
                int beta = Convert.ToInt32(Console.ReadLine());
                if (beta < 0)
                {
                    Console.WriteLine("Недопустимое значение b");
                    goto Beta_in;
                }
                //Шифровка
                Console.WriteLine("Введите текст, который необходимо зашифровать:");
                string text = Console.ReadLine();
                char[] text_encoded = new char[text.Length];
                text_encoded = text.ToCharArray();
                int[] open_codes = new int[text.Length];
                int[] close_codes = new int[text.Length];
                Console.WriteLine("Зашифрованный текст:");
                for (int i = 0; i < text.Length; i++)
                {
                    try
                    {
                        open_codes[i] = eng_abc.BinarySearch(text_encoded[i]);
                        if (open_codes[i] < 0) close_codes[i] = 52;
                        else close_codes[i] = (open_codes[i] * alpha + beta) % n;
                        text_encoded[i] = eng_abc[close_codes[i]];
                        Console.Write(text_encoded[i]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                //Дешифровка
                Console.WriteLine("\nРасшифрованный текст:");
                int alpha_obr = Alpha_Obr(alpha, n);
                if (alpha_obr < 0) alpha_obr = alpha_obr + n;
                for (int i=0; i<text.Length; i++)
                {
                    try
                    {
                        close_codes[i] = eng_abc.BinarySearch(text_encoded[i]);
                        if (close_codes[i] < 0) open_codes[i] = 52;
                        else open_codes[i] = ((close_codes[i] - beta) * alpha_obr) % n;
                        if (open_codes[i] == 52)
                        {
                            text_encoded[i] = eng_abc[open_codes[i]];
                        }
                        else if (open_codes[i] < 0)
                        { 
                            open_codes[i] = open_codes[i] + n;
                            text_encoded[i] = eng_abc[open_codes[i]];
                        }
                        else if (open_codes[i]>=n)
                        {
                            open_codes[i] = open_codes[i] - n;
                            text_encoded[i] = eng_abc[open_codes[i]];
                        }
                        else text_encoded[i] = eng_abc[open_codes[i]];
                        Console.Write($"{text_encoded[i]}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            //Аффинный рекуррентный шифр
            {
                Console.WriteLine("\n\nАффинный рекуррентный шифр");
            N_in1:
                Console.WriteLine("Введите n");
                int n = Convert.ToInt32(Console.ReadLine());
                if (n< 0)
                {
                    Console.WriteLine("Недопустимое значение n");
                    goto N_in1;
                }
                Console.WriteLine("Введите a1");
            Alpha_in1:
                int alpha1 = Convert.ToInt32(Console.ReadLine());
                if (NOD(n, alpha1) == false || alpha1 < 0)
                {
                    Console.WriteLine("Недопустимое значение a1");
                    goto Alpha_in1;
                }
                Console.WriteLine("Введите a2");
            Alpha_in2:
                int alpha2 = Convert.ToInt32(Console.ReadLine());
                if (NOD(n, alpha2) == false || alpha2 < 0)
                {
                    Console.WriteLine("Недопустимое значение a2");
                    goto Alpha_in2;
                }
            Beta_in1:
                Console.WriteLine("Введите b");
                int beta1 = Convert.ToInt32(Console.ReadLine());
                if (beta1 < 0)
                {
                    Console.WriteLine("Недопустимое значение b1");
                    goto Beta_in1;
                }
            Beta_in2:
                Console.WriteLine("Введите b");
                int beta2 = Convert.ToInt32(Console.ReadLine());
                if (beta2 < 0)
                {
                    Console.WriteLine("Недопустимое значение b2");
                    goto Beta_in2;
                }
                //Шифровка
                Console.WriteLine("Введите текст для шифрования:");
                string text = Console.ReadLine();
                char[] text_encoded = new char[text.Length];
                text_encoded = text.ToCharArray();
                int[] open_codes = new int[text.Length];
                int[] close_codes = new int[text.Length];
                int[] alpha = new int[text.Length]; alpha[0] = alpha1; alpha[1] = alpha2;
                int[] beta = new int[text.Length]; beta[0] = beta1; beta[1] = beta2;
                int[] alpha_obr = new int[text.Length];
                Console.WriteLine("Зашифрованный текст:");
                for (int i=0; i<text.Length; i++)
                {
                    try
                    {
                        if (i < 2)
                        {
                            open_codes[i] = eng_abc.BinarySearch(text_encoded[i]);
                            if (open_codes[i] < 0) close_codes[i] = 52;
                            else close_codes[i] = (open_codes[i] * alpha[i] + beta[i]) % n;
                            text_encoded[i] = eng_abc[close_codes[i]];
                            Console.WriteLine($"{open_codes[i]}      {close_codes[i]}      {text_encoded[i]}");
                        }
                        else
                        {
                            open_codes[i] = eng_abc.BinarySearch(text_encoded[i]);
                            if (open_codes[i] < 0)
                            {
                                close_codes[i] = 52;
                                alpha[i] = alpha[i - 1];
                                beta[i] = beta[i - 1];
                            }
                            else
                            {
                                alpha[i] = alpha[i - 1] * alpha[i - 2];
                                if (alpha[i] >= n)
                                {
                                    while (alpha[i] >= n)
                                    {
                                        alpha[i] = alpha[i] - n;
                                    }
                                }
                                beta[i] = beta[i - 1] + beta[i - 2];
                                /*if (beta[i] >= n)
                                {
                                    while (beta[i] >= n)
                                    {
                                        beta[i] = beta[i] - n;
                                    }
                                }*/
                                close_codes[i] = (open_codes[i] * alpha[i] + beta[i]) % n;

                            }
                            text_encoded[i] = eng_abc[close_codes[i]];
                            Console.WriteLine($"{open_codes[i]}      {close_codes[i]}      {text_encoded[i]}        alpha {alpha[i]}        beta {beta[i]}");
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                //Дешифровка
                Console.WriteLine("\nРасшифрованный текст");
                for (int i=0; i<text.Length;i++)
                {
                    try
                    {
                        if (i<2)
                        {
                            alpha_obr[i] = Alpha_Obr(alpha[i], n);
                            close_codes[i] = eng_abc.BinarySearch(text_encoded[i]);
                            if (close_codes[i] < 0)
                            {
                                open_codes[i] = 52;
                                text_encoded[i] = eng_abc[open_codes[i]];
                                Console.Write(text_encoded[i]);
                            }
                            else
                            {
                                open_codes[i] = ((close_codes[i] - beta[i]) * alpha_obr[i]) % n;
                                if (open_codes[i] > n) { open_codes[i] = open_codes[i] - n; text_encoded[i] = eng_abc[open_codes[i]]; }
                                else if (open_codes[i] < 0) { open_codes[i] = open_codes[i] + n; text_encoded[i] = eng_abc[open_codes[i]]; }
                                else text_encoded[i] = eng_abc[open_codes[i]];
                                Console.Write(text_encoded[i]);
                            }
                        }
                        else
                        {
                            
                            close_codes[i] = eng_abc.BinarySearch(text_encoded[i]);
                            if (close_codes[i] < 0)
                            {
                                open_codes[i] = 52;
                                alpha_obr[i] = alpha_obr[i - 1];
                                beta[i] = beta[i - 1];
                                text_encoded[i] = eng_abc[open_codes[i]];
                                Console.Write(text_encoded[i]);
                            }
                            else
                            {
                                alpha[i] = alpha[i - 2] * alpha[i - 1];
                                if (alpha[i] >= 52)
                                {
                                    while (alpha[i] >= 52)
                                    {
                                        alpha[i] = alpha[i] - n;
                                    }
                                }
                                alpha_obr[i] = Alpha_Obr(alpha[i], n);
                                //alpha_obr[i] = alpha_obr[i - 2] * alpha_obr[i - 1];
                                beta[i] = beta[i - 2] + beta[i - 1];
                                if (beta[i] >= n)
                                {
                                    while (beta[i] >= n)
                                    {
                                        beta[i] = beta[i] - n;
                                    }
                                }
                                open_codes[i] = ((close_codes[i] - beta[i]) * alpha_obr[i]) % n;
                                if (open_codes[i] == 52) text_encoded[i] = eng_abc[open_codes[i]];
                                else if (open_codes[i] >= n)
                                {
                                    open_codes[i] = open_codes[i] - n;
                                    text_encoded[i] = eng_abc[open_codes[i]];
                                }
                                else if (open_codes[i] < 0)
                                {
                                    open_codes[i] = open_codes[i] + n;
                                    text_encoded[i] = eng_abc[open_codes[i]];
                                }
                                else text_encoded[i] = eng_abc[open_codes[i]];
                                Console.Write(text_encoded[i]);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

        }
        static bool NOD(int n, int alpha)
        {
            while (n != 0)
            {
                int temp = n;
                n = alpha % n;
                alpha = temp;
            }
            if (alpha == 1) return true;
            else return false;
        }
        static int Alpha_Obr(int alpha, int n)
        {
            int y1 = 1; int y2 = 0;
            while (alpha > 0)
            {
                int q = n / alpha;
                int r = n - q * alpha;
                int y = y2 - q * y1;
                n = alpha;
                alpha = Convert.ToInt32(r);
                y2 = y1;
                y1 = y;
            }
            int d = n; int y_ = y2;
            if (d == 1) return y_;
            else return 0;
        }              
    }
}
