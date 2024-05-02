using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Dvoryanchikov
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public enum Err
        {
            NoError,       //нет ошибок
            RightNotAllow,
            OutofRange,
            SymbolI, // Ожидается символ I
            SymbolF, // Ожидается символ F
            SymbolH, // Ожидается символ H
            SymbolAlnum, // Ожидается символ space или _|A|...|Z
            SymbolTorSpace, // Ожидается символ T или space
            SymbolSemi, // Ожидается символ ;
            SymbolAlnumOrSpecial, // Ожидается символ space или T или A или O или N или ~ или # или & или < или > или =
            SymbolColonOrSpace, // Ожидается символ space или :
            SymbolN, // Ожидается символ N
            SymbolEOrSpace, // Ожидается символ space или E
            SymbolAlnumBracket, // Ожидается символ space или _|A|...|Z или 0|...|9 или [
            SymbolNumOrSpace, // Ожидается символ 0|...|9 или space
            SymbolAssign, // Ожидается символ =
            SymbolD, // Ожидается символ D
            SymbolR, // Ожидается символ R
            SymbolBracketClose, // Ожидается символ ]
            SymbolAlnumBracketClose, // Ожидается символ _|A|...|Z|0|...|9 или ]
            SymbolNumBracketClose, // Ожидается символ 0|...|9 или ]
            SymbolDigit1to9, // Ожидается символ 1|...|9
            SymbolDigit, // Ожидается символ 0|...|9
            SymbolDigit1to9MinusZero, // Ожидается символ 1|...|9 или - или 0
            SymbolO, // Ожидается символ O
            SymbolE, // Ожидается символ E
            SymbolDigit1to9Zero, // Ожидается символ 1|...|9 или 0
            SymbolAssignOrSpace, // Ожидается символ = или space
            SymbolAlnumSpace, // Ожидается символ space или _|A|...|Z|0|...|9
            SymbolNumEOrSpace, // Ожидается символ 0|...|9 или E или space
            SymbolAlnumMinus, // Ожидается символ _|A|...|Z или 0 или 1|...|9 или -
            SymbolT, // Ожидается символ T
            SymbolAlnum1to9MinusZeroOrSpace, // Ожидается символ space или _|A|...|Z или 1|...|9 или 0 или -
            SymbolSpace, // Ожидается символ space
            SymbolNumDotSpaceE, // Ожидается символ 0|...|9 или . или space или E
            SymbolDot, // Ожидается символ .
            SymbolNL, // Ожидается символ N или L
            SymbolS, // Ожидается символ S
            SymbolDotOrSpace, // Ожидается символ . или space
            SymbolAssignSpaceGt, // Ожидается символ = или space или >
            SymbolStop, // Ожидается символ ;
            SymbolDigit0to9ZeroPointE // Ожидается символ 0|...|9 или.или space или E
        }
        public class Result
        {
            private int _ErrPosition;
            private Err _Err;
            private static LinkedList<string> _ListOfDomains;
            public Result(int ErrPosition, Err Err, LinkedList<string> ListOfDomains)
            {
                _ErrPosition = ErrPosition;
                _Err = Err;
                _ListOfDomains = ListOfDomains;
            }
            public int ErrPosition
            {
                get
                {
                    return _ErrPosition;
                }
            }
            public string ErrMessage
            {
                get
                {
                    switch (_Err)
                    {
                        case Err.NoError:
                            { return "^Нет ошибок^"; }
                        case Err.RightNotAllow:
                            { return "Справа лишнее"; }
                        case Err.OutofRange:
                            { return "Выход за границы массива."; }
                        case Err.SymbolI:
                            { return "Ожидается символ I"; }
                        case Err.SymbolF:
                            { return "Ожидается символ F"; }
                        case Err.SymbolH:
                            { return "Ожидается символ H"; }
                        case Err.SymbolAlnum:
                            { return "Ожидается символ space или _|A|...|Z"; }
                        case Err.SymbolTorSpace:
                            { return "Ожидается символ T или space"; }
                        case Err.SymbolSemi:
                            { return "Ожидается символ ;"; }
                        case Err.SymbolAlnumOrSpecial:
                            { return "Ожидается символ space или T или A или O или N или ~ или # или & или < или > или ="; }
                        case Err.SymbolColonOrSpace:
                            { return "Ожидается символ space или :"; }
                        case Err.SymbolN:
                            { return "Ожидается символ N"; }
                        case Err.SymbolEOrSpace:
                            { return "Ожидается символ space или E"; }
                        case Err.SymbolAlnumBracket:
                            { return "Ожидается символ space или _|A|...|Z или 0|...|9 или ["; }
                        case Err.SymbolNumOrSpace:
                            { return "Ожидается символ 0|...|9 или space"; }
                        case Err.SymbolAssign:
                            { return "Ожидается символ ="; }
                        case Err.SymbolD:
                            { return "Ожидается символ D"; }
                        case Err.SymbolR:
                            { return "Ожидается символ R"; }
                        case Err.SymbolBracketClose:
                            { return "Ожидается символ ]"; }
                        case Err.SymbolAlnumBracketClose:
                            { return "Ожидается символ _|A|...|Z|0|...|9 или ]"; }
                        case Err.SymbolNumBracketClose:
                            { return "Ожидается символ 0|...|9 или ]"; }
                        case Err.SymbolDigit1to9:
                            { return "Ожидается символ 1|...|9"; }
                        case Err.SymbolDigit:
                            { return "Ожидается символ 0|...|9"; }
                        case Err.SymbolDigit1to9MinusZero:
                            { return "Ожидается символ 1|...|9 или - или 0"; }
                        case Err.SymbolO:
                            { return "Ожидается символ O"; }
                        case Err.SymbolE:
                            { return "Ожидается символ E"; }
                        case Err.SymbolDigit1to9Zero:
                            { return "Ожидается символ 1|...|9 или 0"; }
                        case Err.SymbolAssignOrSpace:
                            { return "Ожидается символ = или space"; }
                        case Err.SymbolAlnumSpace:
                            { return "Ожидается символ space или _|A|...|Z|0|...|9"; }
                        case Err.SymbolNumEOrSpace:
                            { return "Ожидается символ 0|...|9 или E или space"; }
                        case Err.SymbolAlnumMinus:
                            { return "Ожидается символ _|A|...|Z или 0 или 1|...|9 или -"; }
                        case Err.SymbolT:
                            { return "Ожидается символ T"; }
                        case Err.SymbolAlnum1to9MinusZeroOrSpace:
                            { return "Ожидается символ space или _|A|...|Z или 1|...|9 или 0 или -"; }
                        case Err.SymbolSpace:
                            { return "Ожидается символ space"; }
                        case Err.SymbolNumDotSpaceE:
                            { return "Ожидается символ 0|...|9 или . или space или E"; }
                        case Err.SymbolDot:
                            { return "Ожидается символ ."; }
                        case Err.SymbolNL:
                            { return "Ожидается символ N или L"; }
                        case Err.SymbolS:
                            { return "Ожидается символ S"; }
                        case Err.SymbolDotOrSpace:
                            { return "Ожидается символ . или space"; }
                        case Err.SymbolAssignSpaceGt:
                            { return "Ожидается символ = или space или >"; }
                        case Err.SymbolStop:
                            { return "Ожидается символ ;"; }
                        case Err.SymbolDigit0to9ZeroPointE:
                            { return "Ожидается символ ;"; }
                        default:
                            { return "^Неизвестная ошибка!^"; }
                    }
                }
            }

            public LinkedList<string> ListOfDomains
            {
                get
                {
                    return _ListOfDomains;
                }
            }
        }
        public static string S;
        public static int P;
        public bool logDataGridView = true;
        static class CheckEmailAddress
        {
            private enum EnumState
            {
                Error, F, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15, f16,
                f17, f18, f19, f20, f21, f22, f23, f24, f25, f26, f27, f28, f29, f30, f31, f32, f33, f34,
                f35, f36, f37, f38, f39, f40, f41, f42, f43, f44, f45, f46, f47, f48, f49, f50, f51, f52,
                f53, f54, f55, f56, f57, f58, f59, f60, f61, f62, f63, f64, f65, f66, f67, f68, f69, f70,
                f71, f72, f73, f74, f75, f76, f77, f78, f79, f80, f81, f82, f83, f84, f85, f86, f87, f88,
                f89, f90, f91
            };
            private static int _Pos;
            private static string _Str;
            private static Err _Err;
            private static int _ErrPos;
            private static LinkedList<string> _ListOfDomains;

            public static Result Check(string InputString)
            {
                _Pos = 0;
                _Str = InputString.Replace('\n', ' ');
                _Str = _Str.ToLower();
                _Str += "%";
                S = _Str;
                _ListOfDomains = new LinkedList<string>();
                SetError(Err.NoError, -1);
                EmailAddress();
                return new Result(_ErrPos, _Err, _ListOfDomains);
            }
            private static bool EmailAddress()
            {
                bool logf9space = false;
                EnumState State = EnumState.f1;
                string DomainName = "";
                while ((State != EnumState.Error) && (State != EnumState.F))
                {
                    if (_Pos >= _Str.Length)
                    {
                        SetError(Err.OutofRange, _Pos - 1); State = EnumState.Error;
                    }
                    else
                    {
                        int i = _Pos + 1;
                        switch (State)
                        {
                            case EnumState.f1:
                                {
                                    if (_Str[_Pos] == 'i')
                                    {
                                        State = EnumState.f2;
                                    }
                                    else
                                    {
                                        SetError(Err.SymbolI, i);
                                        State = EnumState.Error;
                                    }
                                    break;
                                }
                            case EnumState.f2:
                                {
                                    if (_Str[_Pos] == 'f')
                                    {
                                        SetError(Err.NoError, i);
                                        State = EnumState.f3;
                                    }
                                    else
                                    {
                                        SetError(Err.SymbolF, i);
                                        State = EnumState.Error;
                                    }
                                    break;
                                }
                            case EnumState.f3:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f4;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolSpace, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f4:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f4;
                                    }
                                    else if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_')
                                    {
                                        State = EnumState.f5;
                                    }
                                    else if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f15;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f14;
                                    }
                                    else if (_Str[_Pos] == '-')
                                    {
                                        State = EnumState.f16;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnum1to9MinusZeroOrSpace, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f5:
                                {
                                    if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_' || char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f5;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f9;
                                    }
                                    else if (_Str[_Pos] == '[')
                                    {
                                        State = EnumState.f6;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumSpace, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f6:
                                {
                                    if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_')
                                    {
                                        State = EnumState.f7;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f10;
                                    }
                                    else if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f13;
                                    }
                                    else if (_Str[_Pos] == '-')
                                    {
                                        State = EnumState.f11;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumMinus, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f7:
                                {
                                    if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_' || char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f7;
                                    }
                                    else if (_Str[_Pos] == ']')
                                    {
                                        State = EnumState.f8;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumBracketClose, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f11:
                                {
                                    if (_Str[_Pos] != '0' && char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f13;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f10:
                                {
                                    if (_Str[_Pos] == ']')
                                    {
                                        State = EnumState.f8;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolBracketClose, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f13:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f13;
                                    }
                                    else if (_Str[_Pos] == ']')
                                    {
                                        State = EnumState.f8;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNumBracketClose, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f8:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f9;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolSpace, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f15:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f15;
                                    }
                                    else if (_Str[_Pos] == '.')
                                    {
                                        State = EnumState.f18;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f9;
                                    }
                                    else if (_Str[_Pos] == 'e')
                                    {
                                        State = EnumState.f20;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit0to9ZeroPointE, _Pos);

                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f16:
                                {
                                    if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f15;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f17;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9Zero, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f17:
                                {
                                    if (_Str[_Pos] == '.')
                                    {
                                        State = EnumState.f18;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDot, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f14:
                                {
                                    if (_Str[_Pos] == '.')
                                    {
                                        State = EnumState.f18;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f8;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDotOrSpace, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }
                            case EnumState.f18:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f19;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit, _Pos);
                                        // err_Pos = i;
                                    }
                                    break;
                                }

                            case EnumState.f19:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f19;
                                    }
                                    else if (_Str[_Pos] == 'e')
                                    {
                                        State = EnumState.f20;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f9;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNumEOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f20:
                                {
                                    if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f22;
                                    }
                                    else if (_Str[_Pos] == '-')
                                    {
                                        State = EnumState.f21;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f8;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9MinusZero, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f21:
                                {
                                    if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f22;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f22:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f22;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f9;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNumOrSpace, _Pos);
                                    }
                                    break;
                                }

                            // And or
                            case EnumState.f9:
                                {
                                    if (_Str[_Pos] == ' ' && !logf9space)
                                    {
                                        logf9space = true;
                                        State = EnumState.f9;
                                    }
                                    else if (_Str[_Pos] == ' ' && logf9space)
                                    {
                                        State = EnumState.f23;
                                    }
                                    else if (_Str[_Pos] == 'a')
                                    {
                                        State = EnumState.f29;
                                    }
                                    else if (_Str[_Pos] == 'o')
                                    {
                                        State = EnumState.f30;
                                    }
                                    else if (_Str[_Pos] == 'n')
                                    {
                                        State = EnumState.f31;
                                    }
                                    else if (_Str[_Pos] == '~')
                                    {
                                        State = EnumState.f26;
                                    }
                                    else if (_Str[_Pos] == '#')
                                    {
                                        State = EnumState.f26;
                                    }
                                    else if (_Str[_Pos] == '&')
                                    {
                                        State = EnumState.f26;
                                    }
                                    else if (_Str[_Pos] == '<')
                                    {
                                        State = EnumState.f24;
                                    }
                                    else if (_Str[_Pos] == '>')
                                    {
                                        State = EnumState.f28;
                                    }
                                    else if (_Str[_Pos] == '=')
                                    {
                                        State = EnumState.f27;
                                    }
                                    else if (_Str[_Pos] == 't')
                                    {
                                        State = EnumState.f23;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumOrSpecial, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f29:
                                {
                                    if (_Str[_Pos] == 'n')
                                    {
                                        State = EnumState.f32;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolN, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f32:
                                {
                                    if (_Str[_Pos] == 'd')
                                    {
                                        State = EnumState.f26;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolD, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f30:
                                {
                                    if (_Str[_Pos] == 'r')
                                    {
                                        State = EnumState.f26;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolR, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f31:
                                {
                                    if (_Str[_Pos] == 'o')
                                    {
                                        State = EnumState.f33;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolO, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f33:
                                {
                                    if (_Str[_Pos] == 't')
                                    {
                                        State = EnumState.f26;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolT, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f24:
                                {
                                    if (_Str[_Pos] == '=')
                                    {
                                        State = EnumState.f26;
                                    }
                                    else if (_Str[_Pos] == '>')
                                    {
                                        State = EnumState.f26;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f25;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAssignSpaceGt, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f28:
                                {
                                    if (_Str[_Pos] == '=')
                                    {
                                        State = EnumState.f26;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f25;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAssignOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f27:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f25;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f26:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f25;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f25:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f25;
                                    }
                                    else if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_')
                                    {
                                        State = EnumState.f39;
                                    }
                                    else if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f37;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f38;
                                    }
                                    else if (_Str[_Pos] == '-')
                                    {
                                        State = EnumState.f34;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnum1to9MinusZeroOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f39:
                                {
                                    if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_' || char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f39;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f41;
                                    }
                                    else if (_Str[_Pos] == '[')
                                    {
                                        State = EnumState.f45;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f45:
                                {
                                    if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_')
                                    {
                                        State = EnumState.f48;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f46;
                                    }
                                    else if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f50;
                                    }
                                    else if (_Str[_Pos] == '-')
                                    {
                                        State = EnumState.f49;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumMinus, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f48:
                                {
                                    if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_' || char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f48;
                                    }
                                    else if (_Str[_Pos] == ']')
                                    {
                                        State = EnumState.f47;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumBracketClose, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f49:
                                {
                                    if (_Str[_Pos] != '0' && char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f50;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f46:
                                {
                                    if (_Str[_Pos] == ']')
                                    {
                                        State = EnumState.f47;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolBracketClose, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f50:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f50;
                                    }
                                    else if (_Str[_Pos] == ']')
                                    {
                                        State = EnumState.f47;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNumBracketClose, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f47:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f41;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f37:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f37;
                                    }
                                    else if (_Str[_Pos] == '.')
                                    {
                                        State = EnumState.f36;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f41;
                                    }
                                    else if (_Str[_Pos] == 'e')
                                    {
                                        State = EnumState.f72;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNumDotSpaceE, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f34:
                                {
                                    if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f37;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f35;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9Zero, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f35:
                                {
                                    if (_Str[_Pos] == '.')
                                    {
                                        State = EnumState.f36;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f41;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDotOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f38:
                                {
                                    if (_Str[_Pos] == '.')
                                    {
                                        State = EnumState.f36;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f41;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDotOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f36:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f40;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f40:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f40;
                                    }
                                    else if (_Str[_Pos] == 'e')
                                    {
                                        State = EnumState.f42;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f41;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNumEOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f42:
                                {
                                    if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f44;
                                    }
                                    else if (_Str[_Pos] == '-')
                                    {
                                        State = EnumState.f43;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f47;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9MinusZero, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f43:
                                {
                                    if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f44;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f44:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f44;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f41;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNumOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f41:
                                {
                                    if (_Str[_Pos] == 't')
                                    {
                                        State = EnumState.f23;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f41;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolTorSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f23:
                                {
                                    if (_Str[_Pos] == 'h')
                                    {
                                        State = EnumState.f51;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolH, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f51:
                                {
                                    if (_Str[_Pos] == 'e')
                                    {
                                        State = EnumState.f52;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolE, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f52:
                                {
                                    if (_Str[_Pos] == 'n')
                                    {
                                        State = EnumState.f53;
                                    }

                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolN, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f53:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f54;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f54:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f54;
                                    }
                                    else if (_Str[_Pos] == '_' || char.IsLetter(_Str[_Pos]))
                                    {
                                        State = EnumState.f55;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnum, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f55:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f56;
                                    }
                                    else if (_Str[_Pos] == '_' || char.IsLetter(_Str[_Pos]) || char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f55;
                                    }
                                    else if (_Str[_Pos] == '[')
                                    {
                                        State = EnumState.f57;
                                    }
                                    else if (_Str[_Pos] == ':')
                                    {
                                        State = EnumState.f63;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumBracket, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f57:
                                {
                                    if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_')
                                    {
                                        State = EnumState.f58;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f60;
                                    }
                                    else if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f62;
                                    }
                                    else if (_Str[_Pos] == '-')
                                    {
                                        State = EnumState.f61;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumMinus, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f58:
                                {
                                    if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_' || char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f58;
                                    }
                                    else if (_Str[_Pos] == ']')
                                    {
                                        State = EnumState.f59;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumBracketClose, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f61:
                                {
                                    if (_Str[_Pos] != '0' && char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f62;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f60:
                                {
                                    if (_Str[_Pos] == ']')
                                    {
                                        State = EnumState.f59;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolBracketClose, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f62:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f62;
                                    }
                                    else if (_Str[_Pos] == ']')
                                    {
                                        State = EnumState.f59;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNumBracketClose, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f59:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f56;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolSpace, _Pos);
                                    }
                                    break;
                                }

                            case EnumState.f56:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f56;
                                    }
                                    else if (_Str[_Pos] == ':')
                                    {
                                        State = EnumState.f63;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolColonOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f63:
                                {
                                    if (_Str[_Pos] == '=')
                                    {
                                        State = EnumState.f64;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAssign, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f64:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f65;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f65:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f65;
                                    }
                                    else if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_')
                                    {
                                        State = EnumState.f76;
                                    }
                                    else if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f75;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f66;
                                    }
                                    else if (_Str[_Pos] == '-')
                                    {
                                        State = EnumState.f68;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnum1to9MinusZeroOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f76:
                                {
                                    if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_' || char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f76;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f67;
                                    }
                                    else if (_Str[_Pos] == '[')
                                    {
                                        State = EnumState.f77;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f77:
                                {
                                    if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_')
                                    {
                                        State = EnumState.f78;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f79;
                                    }
                                    else if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f82;
                                    }
                                    else if (_Str[_Pos] == '-')
                                    {
                                        State = EnumState.f80;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumMinus, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f78:
                                {
                                    if (char.IsLetter(_Str[_Pos]) || _Str[_Pos] == '_' || char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f78;
                                    }
                                    else if (_Str[_Pos] == ']')
                                    {
                                        State = EnumState.f81;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolAlnumBracketClose, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f80:
                                {
                                    if (_Str[_Pos] != '0' && char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f82;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f79:
                                {
                                    if (_Str[_Pos] == ']')
                                    {
                                        State = EnumState.f81;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolBracketClose, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f82:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f82;
                                    }
                                    else if (_Str[_Pos] == ']')
                                    {
                                        State = EnumState.f81;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNumBracketClose, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f81:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f67;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f75:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f75;
                                    }
                                    else if (_Str[_Pos] == '.')
                                    {
                                        State = EnumState.f70;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f67;
                                    }
                                    else if (_Str[_Pos] == 'e')
                                    {
                                        State = EnumState.f72;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNumDotSpaceE, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f68:
                                {
                                    if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f75;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f69;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9Zero, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f70:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f71;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f66:
                                {
                                    if (_Str[_Pos] == '.')
                                    {
                                        State = EnumState.f70;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f67;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDotOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f69:
                                {
                                    if (_Str[_Pos] == '.')
                                    {
                                        State = EnumState.f70;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDot, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f71:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f71;
                                    }
                                    else if (_Str[_Pos] == 'e')
                                    {
                                        State = EnumState.f72;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f67;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNumEOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f72:
                                {
                                    if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f74;
                                    }
                                    else if (_Str[_Pos] == '-')
                                    {
                                        State = EnumState.f73;
                                    }
                                    else if (_Str[_Pos] == '0')
                                    {
                                        State = EnumState.f81;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9MinusZero, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f73:
                                {
                                    if (char.IsDigit(_Str[_Pos]) && _Str[_Pos] != '0')
                                    {
                                        State = EnumState.f74;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolDigit1to9, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f74:
                                {
                                    if (char.IsDigit(_Str[_Pos]))
                                    {
                                        State = EnumState.f74;
                                    }
                                    else if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f67;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNumOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f67:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f67;
                                    }
                                    else if (_Str[_Pos] == 'e')
                                    {
                                        State = EnumState.f83;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolEOrSpace, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f83:
                                {
                                    if (_Str[_Pos] == 'n')
                                    {
                                        State = EnumState.f84;
                                    }
                                    else if (_Str[_Pos] == 'l')
                                    {
                                        State = EnumState.f88;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolNL, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f84:
                                {
                                    if (_Str[_Pos] == 'd')
                                    {
                                        State = EnumState.f85;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolD, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f85:
                                {
                                    if (_Str[_Pos] == ';')
                                    {
                                        State = EnumState.F;
                                        SetError(Err.NoError, 0);
                                        //err_Mes = "^Ошибок не обнаружено^";

                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolSemi, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f88:
                                {
                                    if (_Str[_Pos] == 's')
                                    {
                                        State = EnumState.f89;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolS, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f89:
                                {
                                    if (_Str[_Pos] == 'i')
                                    {
                                        State = EnumState.f90;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolI, _Pos);
                                    }
                                    break;
                                }
                            case EnumState.f90:
                                {
                                    if (_Str[_Pos] == 'f')
                                    {
                                        State = EnumState.f91;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolF, _Pos);
                                    }
                                    break;

                                }
                            case EnumState.f91:
                                {
                                    if (_Str[_Pos] == ' ')
                                    {
                                        State = EnumState.f4;
                                    }
                                    else
                                    {
                                        State = EnumState.Error;
                                        SetError(Err.SymbolSpace, _Pos);
                                    }
                                    break;
                                }
                            default:
                                {
                                    State = EnumState.Error;
                                    break;
                                }
                        }
                    }
                    if (State != EnumState.F) _Pos++;
                }
                //if (_Pos < _Str.Length - 1) SetError(Err.RightNotAllow, _Pos + 1);
                return (State == EnumState.F);
            }

            private static void SetError(Err ErrorType, int ErrorPosition)
            {

                _Err = ErrorType;
                _ErrPos = ErrorPosition;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Result r = CheckEmailAddress.Check(TextBox_Chain.Text);
            if (r.ErrPosition != -1)
            {
                TextBox_Chain.SelectionStart = r.ErrPosition;
                TextBox_Chain.Focus();
            }
            label2.Text = r.ErrMessage;
            //label4.Text = "S = "+S.Length.ToString();
            //label5.Text = "P = "+P.ToString();

            List<string> identifiers = new List<string>();

            //listBox1.Items.Clear();

            string[] idn = Identifier(S);
            string[][] idnn = IdentifierTypeA1(idn);

            if (isThen(idnn[1])) label2.Text = "Индефикатор не может быть зарезервированным словом!";
            else if (isNum(idnn[0])) label2.Text = "Число должно быть в диапозоне -32768 – 32767!";
            else if (lenInd(idnn[1])) label2.Text = "Индефикатор не может быть > 8 символов!";

            string[][] CardReal = letWordCardReal(idnn[1]);
            string[] Real = new string[CardReal[0].Length + idnn[0].Length];

            int countRR = 0;
            while (countRR < Real.Length)
            {
                for (int i=0; i< CardReal[0].Length; i++)
                {
                    Real[countRR] = CardReal[0][i];
                    countRR++;
                }
                for (int i = 0; i < idnn[0].Length; i++)
                {
                    Real[countRR] = idnn[0][i];
                    countRR++;
                }
            }

            if (logDataGridView)
            {
                logDataGridView = false;
                dataGridView1.Columns.Add("Идентификаторы и константы", "Идентификаторы и константы");
                dataGridView1.Columns.Add("Тип", "Тип");
            }

            dataGridView1.Rows.Clear();


            if (!isThen(idnn[1]) && !isNum(idnn[0]) && !lenInd(idnn[1]))
            {
                //listBox1.Items.AddRange(Real.ToArray());
                //listBox1.Items.AddRange(CardReal[0].ToArray());
                //listBox1.Items.AddRange(CardReal[0].ToArray());
                for (int i = 0; i < Real.Length; i++)
                {
                    dataGridView1.Rows.Add(Real[i], "REAL");
                }
                for (int i = 0; i < CardReal[1].Length; i++)
                {
                    dataGridView1.Rows.Add(CardReal[1][i], "CARDINAL");
                }
            }
        }

        public string[] Identifier(string input)
        {
            input = input.Trim();
            string[] parts = input.Split(' ');
            string[] idn;
            if (parts.Length != 0) {
                idn = new string[parts.Length / 2];
                int count = 1;
                for (int i = 0; i<idn.Length; i++)
                {
                    idn[i] = parts[count];
                    count += 2;
                }
            }
            else
            {
                idn = new string[0];
                idn[0] = "-1";
            }
            return idn;

        }

        public string[][] IdentifierTypeA1(string[] input)
        {
            var numberStrings = input.Where(s => char.IsDigit(s[0])).ToList();
            var letterStrings = input.Where(s => char.IsLetter(s[0])).ToList();

            string[] num = new string[numberStrings.Count];
            string[] str = new string[letterStrings.Count];
            for (int i = 0; i < numberStrings.Count; i++)
            {
                num[i] = numberStrings[i].ToString();
            }

            for (int i = 0; i < letterStrings.Count; i++)
            {
                str[i] = letterStrings[i].ToString();
            }

            string[][] result = new string[2][];
            result[0] = num;
            result[1] = str;
            return result;
        }

        public bool isThen(string[] input)
        {
            bool result = false;
            string[] reservWord = {"if", "then", "elsif", "end", "or", "not", "and" };
            for (int i = 0; i < input.Length; i++)
            {
                for (int j=0; j < reservWord.Length; j++)
                {
                    if (input[i] == reservWord[j]) result = true;
                }
            }
            return result;
        }

        public bool isNum(string[] input)
        {
            bool result = false;
            for (int i = 0; i < input.Length; i++)
            {
                int number;
                if (int.TryParse(input[i], out number))
                {

                    if (number > 32767 || number < -32767) result = true;
                }
            }
            return result;
        }

        public bool lenInd(string[] input)
        {
            bool result = false;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Length>8) result = true;
            }
            return result;

        }

        public string[][] letWordCardReal(string[] input)
        {
            int countR = 0;
            int countC = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i].IndexOf('[')) >= 0) countC++;
                countR++;
            }
            string[] real = new string[countR];
            string[] card = new string[countC];

            int cR = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i].IndexOf('[')) >= 0)
                {
                    string ttt = input[i];
                    int kkk = input[i].IndexOf('[')+1;
                    int kkk1 = input[i].IndexOf(']');

                    Console.WriteLine();
                    string k1 = input[i].Substring(kkk);
                    card[cR] = k1.Substring(0, k1.IndexOf(']'));
                    cR++;
                }
            }
            int cC = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i].IndexOf('[')) >= 0)
                {
                    string kk = input[i];
                    real[cC] = input[i].Substring(0, input[i].IndexOf('['));
                    string ccc = real[cC];
                    cC++;
                }
                else
                {
                    real[cC] = input[i];
                    cC++;
                }
            }

            string[][] result = new string[2][];
            result[0] = real;
            result[1] = card;
            return result;
        }
        private void TextBox_Chain_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
