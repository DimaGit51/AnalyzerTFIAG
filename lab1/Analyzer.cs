using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dvoryanchikov
{
    internal class Analyzer
    {
        public enum Err
        {

        }

        private static int err_Pos;
        private static string err_Mes;
        enum State
        {
            Error, F, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16, 
            f17, f18, f19, f20, f21, f22, f23, f24, f25, f26, f27, f28, f29, f30, f31, f32, f33, f34, 
            f35, f36, f37, f38, f39, f40, f41, f42, f43, f44, f45, f46, f47, f48, f49, f50, f51, f52, 
            f53, f54, f55, f56, f57, f58, f59, f60, f61, f62, f63, f64, f65, f66, f67, f68, f69, f70, 
            f71, f72, f73, f74, f75, f76, f77, f78, f79, f80, f81, f82, f83, f84, f85, f86, f87, f88, 
            f89, f90, f91
        }
        public static string Get_Err_Mes()
        {
            return err_Mes;
        }

        public static int Get_Err_Pos()
        {
            return err_Pos;
        }

        public static bool Analizator(string str)
        {
            err_Mes = "";
            string st = str.ToLower()+"%";
            Console.WriteLine(st);
            char chr;
            bool logf9space = false;
            int i = 0;
            State s = State.f1;
            while ((s != State.Error) && (s != State.F))
            {
                chr = st[i];
                i++;
                switch (s)
                {
                    case State.f1:
                        {
                            if (chr == 'i')
                            {
                                s = State.f2;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 'I'";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f2:
                        {
                            if (chr == 'f')
                            {
                                s = State.f3;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 'F'";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f3:
                        {
                            if (chr == ' ')
                            {
                                s = State.f4;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space";
                                err_Pos = i;
                            }
                            break;
                        }
                    
                    case State.f4:
                        {
                            if (chr == ' ')
                            {
                                s = State.f4;
                            }
                            else if (char.IsLetter(chr) || chr == '_')
                            {
                                s = State.f5;
                            }
                            else if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f15;
                            }
                            else if (chr == '0')
                            {
                                s = State.f14;
                            }
                            else if (chr == '-')
                            {
                                s = State.f16;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space или _|A|...|Z или 1|...|9 или 0 или -";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f5:
                        {
                            if (char.IsLetter(chr) || chr == '_' || char.IsDigit(chr))
                            {
                                s = State.f5;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f9;
                            }
                            else if (chr == '[')
                            {
                                s = State.f6;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space или _|A|...|Z|0|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f6:
                        {
                            if (char.IsLetter(chr) || chr == '_')
                            {
                                s = State.f7;
                            }
                            else if (chr == '0')
                            {
                                s = State.f10;
                            }
                            else if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f13;
                            }
                            else if (chr == '-')
                            {
                                s = State.f11;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ _|A|...|Z или 0 или 1|...|9 или -";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f7:
                        {
                            if (char.IsLetter(chr) || chr == '_' || char.IsDigit(chr))
                            {
                                s = State.f7;
                            }
                            else if (chr == ']')
                            {
                                s = State.f8;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ _|A|...|Z|0|...|9 или ]";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f11:
                        {
                            if (chr != '0' && char.IsDigit(chr))
                            {
                                s = State.f13;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f10:
                        {
                            if (chr == ']')
                            {
                                s = State.f8;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ ]";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f13:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f13;
                            }
                            else if (chr == ']')
                            {
                                s = State.f8;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или ]";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f8:
                        {
                            if (chr == ' ')
                            {
                                s = State.f9;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f15:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f15;
                            }
                            else if (chr == '.')
                            {
                                s = State.f18;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f9;
                            }
                            else if (chr == 'e')
                            {
                                s = State.f20;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или . или space или E";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f16:
                        {
                            if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f15;
                            }
                            else if (chr == '0')
                            {
                                s = State.f17;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9 или 0";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f17:
                        {
                            if (chr == '.')
                            {
                                s = State.f18;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ .";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f14:
                        {
                            if (chr == '.')
                            {
                                s = State.f18;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f8;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ . или space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f18:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f19;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f19:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f19;
                            }
                            else if (chr == 'e')
                            {
                                s = State.f20;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f9;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или E или space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f20:
                        {
                            if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f22;
                            }
                            else if (chr == '-')
                            {
                                s = State.f21;
                            }
                            else if (chr == '0')
                            {
                                s = State.f8;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9 или - или 0";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f21:
                        {
                            if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f22;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f22:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f22;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f9;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или space";
                                err_Pos = i;
                            }
                            break;
                        }

                    //And or
                    case State.f9:
                        {
                            if (chr == ' ' && !logf9space)
                            {
                                logf9space = true;
                                s = State.f9;
                            }
                            else if (chr == ' ' && logf9space)
                            {
                                s = State.f23;
                            }
                            else if (chr == 'a')
                            {
                                s = State.f29;
                            }
                            else if (chr == 'o')
                            {
                                s = State.f30;
                            }
                            else if (chr == 'n')
                            {
                                s = State.f31;
                            }
                            else if (chr == '~')
                            {
                                s = State.f26;
                            }
                            else if (chr == '#')
                            {
                                s = State.f26;
                            }
                            else if (chr == '&')
                            {
                                s = State.f26;
                            }
                            else if (chr == '<')
                            {
                                s = State.f24;
                            }
                            else if (chr == '>')
                            {
                                s = State.f28;
                            }
                            else if (chr == '=')
                            {
                                s = State.f27;
                            }
                            else if (chr == 't')
                            {
                                s = State.f23;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space или T или A или O или N или ~ или # или & или < или > или =";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f29:
                        {
                            if (chr == 'n')
                            {
                                s = State.f32;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ N";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f32:
                        {
                            if (chr == 'd')
                            {
                                s = State.f26;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ D";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f30:
                        {
                            if (chr == 'r')
                            {
                                s = State.f26;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ R";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f31:
                        {
                            if (chr == 'o')
                            {
                                s = State.f33;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ O";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f33:
                        {
                            if (chr == 't')
                            {
                                s = State.f26;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ T";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f24:
                        {
                            if (chr == '=')
                            {
                                s = State.f26;
                            }
                            else if (chr == '>')
                            {
                                s = State.f26;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f25;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ = или space или > ";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f28:
                        {
                            if (chr == '=')
                            {
                                s = State.f26;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f25;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ = или space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f27:
                        {
                            if (chr == ' ')
                            {
                                s = State.f25;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f26:
                        {
                            if (chr == ' ')
                            {
                                s = State.f25;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space";
                                err_Pos = i;
                            }
                            break;
                        }

                    // GPT Dop start
                    case State.f25:
                        {
                            if (chr == ' ')
                            {
                                s = State.f25;
                            }
                            else if (char.IsLetter(chr) || chr == '_')
                            {
                                s = State.f39;
                            }
                            else if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f37;
                            }
                            else if (chr == '0')
                            {
                                s = State.f38;
                            }
                            else if (chr == '-')
                            {
                                s = State.f34;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space или _|A|...|Z или 1|...|9 или 0 или -";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f39:
                        {
                            if (char.IsLetter(chr) || chr == '_' || char.IsDigit(chr))
                            {
                                s = State.f39;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f41;
                            }
                            else if (chr == '[')
                            {
                                s = State.f45;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space или _|A|...|Z|0|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f45:
                        {
                            if (char.IsLetter(chr) || chr == '_')
                            {
                                s = State.f48;
                            }
                            else if (chr == '0')
                            {
                                s = State.f46;
                            }
                            else if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f50;
                            }
                            else if (chr == '-')
                            {
                                s = State.f49;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ _|A|...|Z или 0 или 1|...|9 или -";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f48:
                        {
                            if (char.IsLetter(chr) || chr == '_' || char.IsDigit(chr))
                            {
                                s = State.f48;
                            }
                            else if (chr == ']')
                            {
                                s = State.f47;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ _|A|...|Z|0|...|9 или ]";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f49:
                        {
                            if (chr != '0' && char.IsDigit(chr))
                            {
                                s = State.f50;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f46:
                        {
                            if (chr == ']')
                            {
                                s = State.f47;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ ]";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f50:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f50;
                            }
                            else if (chr == ']')
                            {
                                s = State.f47;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или ]";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f47:
                        {
                            if (chr == ' ')
                            {
                                s = State.f41;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f37:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f37;
                            }
                            else if (chr == '.')
                            {
                                s = State.f36;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f41;
                            }
                            else if (chr == 'e')
                            {
                                s = State.f72;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или . или space или E";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f34:
                        {
                            if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f37;
                            }
                            else if (chr == '0')
                            {
                                s = State.f35;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9 или 0";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f35:
                        {
                            if (chr == '.')
                            {
                                s = State.f36;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f41;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ . или space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f38:
                        {
                            if (chr == '.')
                            {
                                s = State.f36;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f41;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ . или space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f36:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f40;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f40:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f40;
                            }
                            else if (chr == 'e')
                            {
                                s = State.f42;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f41;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или E или space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f42:
                        {
                            if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f44;
                            }
                            else if (chr == '-')
                            {
                                s = State.f43;
                            }
                            else if (chr == '0')
                            {
                                s = State.f47;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9 или - или 0";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f43:
                        {
                            if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f44;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f44:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f44;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f41;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f41:
                        {
                            if (chr == 't')
                            {
                                s = State.f23;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f41;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ T или space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f23:
                        {
                            if (chr == 'h')
                            {
                                s = State.f51;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ H";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f51:
                        {
                            if (chr == 'e')
                            {
                                s = State.f52;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ E";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f52:
                        {
                            if (chr == 'n')
                            {
                                s = State.f53;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ N";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f53:
                        {
                            if (chr == ' ')
                            {
                                s = State.f54;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f54:
                        {
                            if (chr == ' ')
                            {
                                s = State.f54;
                            }
                            else if (chr == '_' || char.IsLetter(chr))
                            {
                                s = State.f55;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space или _|A|...|Z";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f55:
                        {
                            if (chr == ' ')
                            {
                                s = State.f56;
                            }
                            else if (chr == '_' || char.IsLetter(chr) || char.IsDigit(chr))
                            {
                                s = State.f55;
                            }
                            else if (chr == '[')
                            {
                                s = State.f57;
                            }
                            else if (chr == ':')
                            {
                                s = State.f63;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space или _|A|...|Z или 0|...|9 или [";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f57:
                        {
                            if (char.IsLetter(chr) || chr == '_')
                            {
                                s = State.f58;
                            }
                            else if (chr == '0')
                            {
                                s = State.f60;
                            }
                            else if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f62;
                            }
                            else if (chr == '-')
                            {
                                s = State.f61;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ _|A|...|Z или 0 или 1|...|9 или -";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f58:
                        {
                            if (char.IsLetter(chr) || chr == '_' || char.IsDigit(chr))
                            {
                                s = State.f58;
                            }
                            else if (chr == ']')
                            {
                                s = State.f59;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ _|A|...|Z|0|...|9 или ]";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f61:
                        {
                            if (chr != '0' && char.IsDigit(chr))
                            {
                                s = State.f62;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f60:
                        {
                            if (chr == ']')
                            {
                                s = State.f59;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ ]";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f62:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f62;
                            }
                            else if (chr == ']')
                            {
                                s = State.f59;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или ]";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f59:
                        {
                            if (chr == ' ')
                            {
                                s = State.f56;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f56:
                        {
                            if (chr == ' ')
                            {
                                s = State.f56;
                            }
                            else if (chr == ':')
                            {
                                s = State.f63;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space или :";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f63:
                        {
                            if (chr == '=')
                            {
                                s = State.f64;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ =";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f64:
                        {
                            if (chr == ' ')
                            {
                                s = State.f65;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f65:
                        {
                            if (chr == ' ')
                            {
                                s = State.f65;
                            }
                            else if (char.IsLetter(chr) || chr == '_')
                            {
                                s = State.f76;
                            }
                            else if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f75;
                            }
                            else if (chr == '0')
                            {
                                s = State.f66;
                            }
                            else if (chr == '-')
                            {
                                s = State.f68;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space или _|A|...|Z или 1|...|9 или 0 или -";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f76:
                        {
                            if (char.IsLetter(chr) || chr == '_' || char.IsDigit(chr))
                            {
                                s = State.f76;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f67;
                            }
                            else if (chr == '[')
                            {
                                s = State.f77;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space или _|A|...|Z|0|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f77:
                        {
                            if (char.IsLetter(chr) || chr == '_')
                            {
                                s = State.f78;
                            }
                            else if (chr == '0')
                            {
                                s = State.f79;
                            }
                            else if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f82;
                            }
                            else if (chr == '-')
                            {
                                s = State.f80;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ _|A|...|Z или 0 или 1|...|9 или -";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f78:
                        {
                            if (char.IsLetter(chr) || chr == '_' || char.IsDigit(chr))
                            {
                                s = State.f78;
                            }
                            else if (chr == ']')
                            {
                                s = State.f81;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ _|A|...|Z|0|...|9 или ]";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f80:
                        {
                            if (chr != '0' && char.IsDigit(chr))
                            {
                                s = State.f82;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f79:
                        {
                            if (chr == ']')
                            {
                                s = State.f81;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ ]";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f82:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f82;
                            }
                            else if (chr == ']')
                            {
                                s = State.f81;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или ]";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f81:
                        {
                            if (chr == ' ')
                            {
                                s = State.f67;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f75:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f75;
                            }
                            else if (chr == '.')
                            {
                                s = State.f70;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f67;
                            }
                            else if (chr == 'e')
                            {
                                s = State.f72;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или . или space или E";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f68:
                        {
                            if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f75;
                            }
                            else if (chr == '0')
                            {
                                s = State.f69;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9 или 0";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f70:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f71;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f66:
                        {
                            if (chr == '.')
                            {
                                s = State.f70;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f67;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ . или space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f69:
                        {
                            if (chr == '.')
                            {
                                s = State.f70;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ .";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f71:
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f71;
                            }
                            else if (chr == 'e')
                            {
                                s = State.f72;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f67;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или E или space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f72:
                        {
                            if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f74;
                            }
                            else if (chr == '-')
                            {
                                s = State.f73;
                            }
                            else if (chr == '0')
                            {
                                s = State.f81;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9 или - или 0";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f73:
                        {
                            if (char.IsDigit(chr) && chr != '0')
                            {
                                s = State.f74;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 1|...|9";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f74:                 
                        {
                            if (char.IsDigit(chr))
                            {
                                s = State.f74;
                            }
                            else if (chr == ' ')
                            {
                                s = State.f67;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ 0|...|9 или space";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f67:
                        {
                            if (chr == ' ')
                            {
                                s = State.f67;
                            }
                            else if (chr == 'e')
                            {
                                s = State.f83;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space или E";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f83:
                        {
                            if (chr == 'n')
                            {
                                s = State.f84;
                            }
                            else if (chr == 'l')
                            {
                                s = State.f88;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ N или L";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f84:
                        {
                            if (chr == 'd')
                            {
                                s = State.f85;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ D";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f85:
                        {
                            if (chr == ';')
                            {
                                s = State.F;
                                err_Mes = "^Ошибок не обнаружено^";

                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ ;";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f88:
                        {
                            if (chr == 's')
                            {
                                s = State.f89;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ S";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f89:
                        {
                            if (chr == 'i')
                            {
                                s = State.f90;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ I";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f90:
                        {
                            if (chr == 'f')
                            {
                                s = State.f91;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ F";
                                err_Pos = i;
                            }
                            break;
                        }
                    case State.f91:
                        {
                            if (chr == ' ')
                            {
                                s = State.f4;
                            }
                            else
                            {
                                s = State.Error;
                                err_Mes = "Ожидается символ space";
                                err_Pos = i;
                            }
                            break;
                        }

                    default:
                        err_Mes = "^Неизвестная ошибка!^";
                        break;
                }
            }
            return (s == State.F);


        }
        public static void ErrMessage()
        {

        }

    }
}
