//using System.Collections.Generic;
//using System;
//using System.Diagnostics.Eventing.Reader;
//using System.Windows.Forms;

//namespace WinFormsApp1
//{
//    public partial class Form1 : Form
//    {
//        public Form1()
//        {
//            InitializeComponent();
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            Result r = CheckEmailAddress.Check(textBox1.Text);
//            if (r.ErrPosition != -1)
//            {
//                textBox1.SelectionStart = r.ErrPosition;
//                textBox1.Focus();
//            }
//            label3.Text = textBox1.Text;
//            label1.Text = r.ErrPosition.ToString();
//            label2.Text = r.ErrMessage;
//            string[] m = r.ListOfDomains.ToArray();
//            string tmp = "";
//            for (int i = 0; i < m.Length; i++)
//            {
//                tmp += m[i];
//            }
//            label3.Text = tmp;
//        }
//        public enum Err
//        {
//            NoError, OutofRange, RightNotAllow, SpaceLetterExpected, TypeExpected, LetterExpected, SpaceUnderlineLetterExpected, SpaceUnderlineLetterDigitDoubledotCommaExpected,
//            LetterDigitExpected, DotHyphenLetterDigitAtExpected, SpaceCommaBracketExpected, CommaSpaceDoubledotExpected, SpaceHyphenDigitExpected,
//            DotHyphenLetterDigitExpected, OverflowDomains, NullExpected, NullSpaceExpected, SpaceDotcommaExpected, SpaceDotExpected, DotExpected,
//            UnderflowDomains, UnrecognizedError, SpaceExpected, BracketSpaceExpected, Digit, DotSpaceDigitExpected
//        }

//        public class Result
//        {
//            private int _ErrPosition;
//            private Err _Err;
//            private static LinkedList<string> _ListOfDomains;
//            public Result(int ErrPosition, Err Err, LinkedList<string> ListOfDomains)
//            {
//                _ErrPosition = ErrPosition;
//                _Err = Err;
//                _ListOfDomains = ListOfDomains;
//            }
//            public int ErrPosition
//            {
//                get
//                {
//                    return _ErrPosition;
//                }
//            }
//            public string ErrMessage
//            {
//                get
//                {
//                    switch (_Err)
//                    {
//                        case Err.SpaceExpected:
//                            { return "Ожидался пробел"; }
//                        case Err.TypeExpected:
//                            { return "Ожидается тип"; }
//                        case Err.NullSpaceExpected:
//                            { return "Ожидали пробел или нолик"; }
//                        case Err.SpaceCommaBracketExpected:
//                            { return "Ожидали запятую, пробел, скобку"; }
//                        case Err.DotExpected:
//                            { return "Точку ждали"; }
//                        case Err.DotSpaceDigitExpected:
//                            { return "Точку, пробел, число ожидали"; }
//                        case Err.SpaceDotExpected:
//                            { return "Пробел,точка ожидались"; }
//                        case Err.BracketSpaceExpected:
//                            { return "Ожидалась скобка или пробел"; }
//                        case Err.NoError:
//                            { return "Нет ошибок"; }
//                        case Err.OutofRange:
//                            { return "Выход за границы массива."; }
//                        case Err.SpaceLetterExpected:
//                            { return "Ожидалась буква или пробел"; }
//                        case Err.SpaceDotcommaExpected: { return "Ожидался пробел или точка с запятой"; }
//                        case Err.LetterExpected:
//                            { return "Ожидали букву."; }
//                        case Err.Digit:
//                            { return "Ожидали число"; }
//                        case Err.LetterDigitExpected:
//                            { return "Букву или цифру ожидали."; }
//                        case Err.SpaceUnderlineLetterExpected:
//                            { return "Ожидали пробел, нижнее подчёркивание или букву"; }
//                        case Err.DotHyphenLetterDigitAtExpected:
//                            { return "Ожидается буква, цифра, точка, дефис или эт."; }
//                        case Err.DotHyphenLetterDigitExpected:
//                            { return "Ожидается буква, цифра, точка или дефис."; }
//                        case Err.UnrecognizedError:
//                            { return "Неизвестная ошибка."; }
//                        case Err.CommaSpaceDoubledotExpected:
//                            { return "Ожидали запятую, пробелл, двоеточие"; }
//                        case Err.SpaceUnderlineLetterDigitDoubledotCommaExpected:
//                            { return "Ожидался пробел, подчёркивание, буква, цифра, двоеточие,запятая"; }
//                        case Err.NullExpected:
//                            { return "Ожидали ноль"; }
//                        case Err.SpaceHyphenDigitExpected:
//                            { return "Ожидали пробел, дефис, число"; }
//                        case Err.RightNotAllow:
//                            {
//                                return "Справа лишнее";
//                            }
//                        default:
//                            { return "Неизвестная ошибка."; }
//                    }
//                }
//            }

//            public LinkedList<string> ListOfDomains
//            {
//                get
//                {
//                    return _ListOfDomains;
//                }
//            }
//        }

//        static class CheckEmailAddress
//        {
//            private enum EnumState { Start, Error, Final, x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15, x16, x17, x18, x19, x20, x21, x22, x23, x24, x25, x26, x27, x28, x29, x30, x31, x32, x33, x34, x35, x36, x37, x38, x39, x40, x41, x42, x43, x44, x45, x46, x462, x47, x48, x49, x50, x51, x52, x53, x54, x55, x56, x57, x58, x59, x60, x61, x62, x63, x64, x65, x66, x67, x68, x69, x70, x71, x72, x73, x74, x75, x76, x77, x78, x79, x80, x81, x82, x83, x84, x85, x86, x87, x88, x89, x90, x91, x92, x93, x94, x95, x96, x97, x98, x99 };
//            private static int _Pos;
//            private static string _Str;
//            private static Err _Err;
//            private static int _ErrPos;
//            private static LinkedList<string> _ListOfDomains;

//            public static Result Check(string InputString)
//            {
//                _Pos = 0;
//                _Str = InputString;
//                _ListOfDomains = new LinkedList<string>();
//                SetError(Err.NoError, -1);
//                EmailAddress();
//                return new Result(_ErrPos, _Err, _ListOfDomains);
//            }
//            private static bool EmailAddress()
//            {
//                EnumState State = EnumState.Start;
//                string DomainName = "";
//                while ((State != EnumState.Error) && (State != EnumState.Final))
//                {
//                    if (_Pos >= _Str.Length)
//                    {
//                        SetError(Err.OutofRange, _Pos - 1); State = EnumState.Error;

//                    }
//                    else
//                    {

//                        switch (State)
//                        {
//                            case EnumState.Start:
//                                {
//                                    if (_Str[_Pos] == 'V')
//                                    {
//                                        State = EnumState.x2;
//                                    }
//                                    else
//                                    {
//                                        SetError(Err.LetterExpected, _Pos);
//                                        State = EnumState.Error;
//                                    }
//                                    break;
//                                }
//                            case EnumState.x2:
//                                {
//                                    if (_Str[_Pos] == 'A')
//                                    {

//                                        State = EnumState.x3;
//                                    }
//                                    else
//                                    {
//                                        SetError(Err.LetterExpected, _Pos);

//                                        State = EnumState.Error;
//                                    }
//                                    break;
//                                }
//                            case EnumState.x3:
//                                {
//                                    if (_Str[_Pos] == 'R')
//                                    {
//                                        State = EnumState.x4;
//                                    }
//                                    else
//                                    {
//                                        SetError(Err.LetterExpected, _Pos);

//                                        State = EnumState.Error;
//                                    }
//                                    break;
//                                }
//                            case EnumState.x4:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x5; }
//                                    else
//                                    {
//                                        SetError(Err.SpaceExpected, _Pos);
//                                        State = EnumState.Error;
//                                    }
//                                    break;
//                                }
//                            case EnumState.x5:
//                                {
//                                    if (_Str[_Pos] == ' ')
//                                    {
//                                        State = EnumState.x5;
//                                    }
//                                    else if ((_Str[_Pos] == '_') || char.IsLetter(_Str[_Pos]))
//                                    {
//                                        State = EnumState.x7;
//                                    }
//                                    else
//                                    {
//                                        SetError(Err.SpaceUnderlineLetterExpected, _Pos);
//                                        State = EnumState.Error;
//                                    }
//                                    break;
//                                }
//                            case EnumState.x7:
//                                {
//                                    if (char.IsLetterOrDigit(_Str[_Pos]) || (_Str[_Pos] == '_'))
//                                    {
//                                        State = EnumState.x7;
//                                    }
//                                    else if (_Str[_Pos] == ',') { State = EnumState.x5; }
//                                    else if (_Str[_Pos] == ' ')
//                                    {

//                                        State = EnumState.x10;

//                                    }
//                                    else if (_Str[_Pos] == ':')
//                                    {

//                                        State = EnumState.x11;
//                                    }
//                                    else
//                                    {
//                                        SetError(Err.SpaceUnderlineLetterDigitDoubledotCommaExpected, _Pos);
//                                        State = EnumState.Error;
//                                    }
//                                    break;
//                                }
//                            case EnumState.x10:
//                                {
//                                    if (_Str[_Pos] == ',')
//                                    {
//                                        State = EnumState.x5;

//                                    }
//                                    else if (_Str[_Pos] == ' ') { State = EnumState.x10; }
//                                    else if (_Str[_Pos] == ':') { State = EnumState.x11; }
//                                    else
//                                    {
//                                        SetError(Err.CommaSpaceDoubledotExpected, _Pos);
//                                        State = EnumState.Error;
//                                    }
//                                    break;
//                                }
//                            case EnumState.x11:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x11; }
//                                    else if (_Str[_Pos] == 'R') { State = EnumState.x28; }
//                                    else if (_Str[_Pos] == 'C') { State = EnumState.x32; }
//                                    else if (_Str[_Pos] == 'D') { State = EnumState.x36; }
//                                    else if (_Str[_Pos] == 'A') { State = EnumState.x42; }
//                                    else if (_Str[_Pos] == 'B') { State = EnumState.x13; }
//                                    else if (_Str[_Pos] == 'W') { State = EnumState.x17; }
//                                    else if (_Str[_Pos] == 'I') { State = EnumState.x21; }
//                                    else
//                                    {

//                                        SetError(Err.TypeExpected, _Pos);
//                                        State = EnumState.Error;
//                                    }



//                                    break;
//                                }
//                            case EnumState.x28:
//                                {
//                                    if (_Str[_Pos] == 'E') { State = EnumState.x29; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x29:
//                                {
//                                    if (_Str[_Pos] == 'A') { State = EnumState.x30; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x30:
//                                {
//                                    if (_Str[_Pos] == 'L') { State = EnumState.x31; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x31:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x31; }
//                                    else if (_Str[_Pos] == ';') { State = EnumState.x99; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotcommaExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x32:
//                                {
//                                    if (_Str[_Pos] == 'H') { State = EnumState.x33; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x33:
//                                {
//                                    if (_Str[_Pos] == 'A') { State = EnumState.x34; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;

//                                }
//                            case EnumState.x34:
//                                {
//                                    if (_Str[_Pos] == 'R') { State = EnumState.x35; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x35:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x35; }
//                                    else if (_Str[_Pos] == ';') { State = EnumState.x99; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotcommaExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x36:
//                                {
//                                    if (_Str[_Pos] == 'O') { State = EnumState.x37; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x37:
//                                {
//                                    if (_Str[_Pos] == 'U') { State = EnumState.x38; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x38:
//                                {
//                                    if (_Str[_Pos] == 'B') { State = EnumState.x39; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x39:
//                                {
//                                    if (_Str[_Pos] == 'L') { State = EnumState.x40; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x40:
//                                {
//                                    if (_Str[_Pos] == 'E') { State = EnumState.x41; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x41:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x41; }
//                                    else if (_Str[_Pos] == ';') { State = EnumState.x99; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotcommaExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x42:
//                                {
//                                    if (_Str[_Pos] == 'R') { State = EnumState.x43; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x43:
//                                {
//                                    if (_Str[_Pos] == 'R') { State = EnumState.x44; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x44:
//                                {
//                                    if (_Str[_Pos] == 'A') { State = EnumState.x45; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x45:
//                                {
//                                    if (_Str[_Pos] == 'Y') { State = EnumState.x46; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x46:
//                                {
//                                    if (_Str[_Pos] == '[') { State = EnumState.x47; }  //// !!!!!!
//                                    else if (_Str[_Pos] == ' ') { State = EnumState.x46; }
//                                    else { State = EnumState.Error; SetError(Err.BracketSpaceExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x47:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x47; }
//                                    else if (char.IsDigit(_Str[_Pos]) || (_Str[_Pos] == 0)) { State = EnumState.x50; }
//                                    else if (_Str[_Pos] == 0) { State = EnumState.x55; }
//                                    else if (_Str[_Pos] == '-') { State = EnumState.x462; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceHyphenDigitExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x462:
//                                {
//                                    if (char.IsDigit(_Str[_Pos]) || (_Str[_Pos] != 0)) { State = EnumState.x50; }
//                                    else { State = EnumState.Error; SetError(Err.Digit, _Pos); }
//                                    break;

//                                }
//                            case EnumState.x50:
//                                {
//                                    if (char.IsDigit(_Str[_Pos])) { State = EnumState.x50; }
//                                    else if (_Str[_Pos] == ' ') { State = EnumState.x55; }
//                                    else if (_Str[_Pos] == '.') { State = EnumState.x51; }
//                                    else { State = EnumState.Error; SetError(Err.DotSpaceDigitExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x55:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x55; }
//                                    else if (_Str[_Pos] == '.') { State = EnumState.x51; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x51:
//                                {
//                                    if (_Str[_Pos] == '.') { State = EnumState.x56; }
//                                    else { State = EnumState.Error; SetError(Err.DotExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x56:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x56; }
//                                    else if (char.IsDigit(_Str[_Pos]) || _Str[_Pos] != 0) { State = EnumState.x57; }
//                                    else if (_Str[_Pos] == 0) { State = EnumState.x58; }
//                                    else if (_Str[_Pos] == '-') { State = EnumState.x54; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceHyphenDigitExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x54:
//                                {
//                                    if (char.IsDigit(_Str[_Pos]) || _Str[_Pos] != 0) { State = EnumState.x57; }
//                                    else { State = EnumState.Error; SetError(Err.Digit, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x57:
//                                {
//                                    if (char.IsDigit(_Str[_Pos])) { State = EnumState.x57; }
//                                    else if (_Str[_Pos] == ' ') { State = EnumState.x58; }
//                                    else if (_Str[_Pos] == ',') { State = EnumState.x47; }
//                                    else if (_Str[_Pos] == ']') { State = EnumState.x59; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceCommaBracketExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x58:
//                                {
//                                    if (_Str[_Pos] == ']') { State = EnumState.x59; }
//                                    else if (_Str[_Pos] == ',') { State = EnumState.x47; }
//                                    else if (_Str[_Pos] == ' ') { State = EnumState.x58; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceCommaBracketExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x59:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x59; }
//                                    else if (_Str[_Pos] == 'O') { State = EnumState.x48; }
//                                    else { State = EnumState.Error; SetError(Err.NullSpaceExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x48:
//                                {
//                                    if (_Str[_Pos] == 'F') { State = EnumState.x49; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x49:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x68; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x68:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x68; }
//                                    else if (_Str[_Pos] == 'B') { State = EnumState.x69; }
//                                    else if (_Str[_Pos] == 'W') { State = EnumState.x73; }
//                                    else if (_Str[_Pos] == 'I') { State = EnumState.x77; }
//                                    else if (_Str[_Pos] == 'R') { State = EnumState.x85; }
//                                    else if (_Str[_Pos] == 'C') { State = EnumState.x89; }
//                                    else if (_Str[_Pos] == 'D') { State = EnumState.x93; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceLetterExpected, _Pos); }
//                                    break;

//                                }
//                            case EnumState.x69:
//                                {
//                                    if (_Str[_Pos] == 'Y') { State = EnumState.x70; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x70:
//                                {
//                                    if (_Str[_Pos] == 'T') { State = EnumState.x71; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x71:
//                                {
//                                    if (_Str[_Pos] == 'E') { State = EnumState.x72; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x72:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x72; }
//                                    if (_Str[_Pos] == ';') { State = EnumState.x99; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotcommaExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x73:
//                                {
//                                    if (_Str[_Pos] == 'O') { State = EnumState.x74; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x74:
//                                {
//                                    if (_Str[_Pos] == 'R') { State = EnumState.x75; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x75:
//                                {
//                                    if (_Str[_Pos] == 'D') { State = EnumState.x76; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x76:
//                                {
//                                    if (_Str[_Pos] == ';') { State = EnumState.x99; }
//                                    else if (_Str[_Pos] == ' ') { State = EnumState.x76; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotcommaExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x77:
//                                {
//                                    if (_Str[_Pos] == 'N') { State = EnumState.x78; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x78:
//                                {
//                                    if (_Str[_Pos] == 'T') { State = EnumState.x79; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x79:
//                                {
//                                    if (_Str[_Pos] == 'E') { State = EnumState.x80; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x80:
//                                {
//                                    if (_Str[_Pos] == 'G') { State = EnumState.x81; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x81:
//                                {
//                                    if (_Str[_Pos] == 'E') { State = EnumState.x82; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x82:
//                                {
//                                    if (_Str[_Pos] == 'R') { State = EnumState.x83; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x83:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x83; }
//                                    else if (_Str[_Pos] == ';') { State = EnumState.x99; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotcommaExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x85:
//                                {
//                                    if (_Str[_Pos] == 'E') { State = EnumState.x86; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x86:
//                                {
//                                    if (_Str[_Pos] == 'A') { State = EnumState.x87; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x87:
//                                {
//                                    if (_Str[_Pos] == 'L') { State = EnumState.x88; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x88:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x88; }
//                                    else if (_Str[_Pos] == ';') { State = EnumState.x99; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotcommaExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x89:
//                                {
//                                    if (_Str[_Pos] == 'H') { State = EnumState.x90; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x90:
//                                {
//                                    if (_Str[_Pos] == 'A') { State = EnumState.x91; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;

//                                }
//                            case EnumState.x91:
//                                {
//                                    if (_Str[_Pos] == 'R') { State = EnumState.x92; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x92:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x92; }
//                                    else if (_Str[_Pos] == ';') { State = EnumState.x99; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotcommaExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x93:
//                                {
//                                    if (_Str[_Pos] == 'O') { State = EnumState.x94; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x94:
//                                {
//                                    if (_Str[_Pos] == 'U') { State = EnumState.x95; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x95:
//                                {
//                                    if (_Str[_Pos] == 'B') { State = EnumState.x96; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x96:
//                                {
//                                    if (_Str[_Pos] == 'L') { State = EnumState.x97; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x97:
//                                {
//                                    if (_Str[_Pos] == 'E') { State = EnumState.x98; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x98:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x98; }
//                                    else if (_Str[_Pos] == ';') { State = EnumState.x99; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotcommaExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x99:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x99; }
//                                    else if ((char.IsLetter(_Str[_Pos]) && (_Str[_Pos] != 'F')) || (_Str[_Pos] == '_')) { State = EnumState.x7; }
//                                    else if ((_Str[_Pos] == 'F')) { State = EnumState.Final; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceUnderlineLetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x13:
//                                {
//                                    if (_Str[_Pos] == 'Y') { State = EnumState.x14; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x14:
//                                {
//                                    if (_Str[_Pos] == 'T') { State = EnumState.x15; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x15:
//                                {
//                                    if (_Str[_Pos] == 'E') { State = EnumState.x16; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x16:
//                                {
//                                    if (_Str[_Pos] == ';') { State = EnumState.x99; }
//                                    else if (_Str[_Pos] == ' ') { State = EnumState.x16; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotcommaExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x17:
//                                {
//                                    if (_Str[_Pos] == 'O') { State = EnumState.x18; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x18:
//                                {
//                                    if (_Str[_Pos] == 'R') { State = EnumState.x19; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x19:
//                                {
//                                    if (_Str[_Pos] == 'D') { State = EnumState.x20; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x20:
//                                {
//                                    if (_Str[_Pos] == ';') { State = EnumState.x99; }
//                                    else if (_Str[_Pos] == ' ') { State = EnumState.x20; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotcommaExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x21:
//                                {
//                                    if (_Str[_Pos] == 'N') { State = EnumState.x22; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x22:
//                                {
//                                    if (_Str[_Pos] == 'T') { State = EnumState.x23; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x23:
//                                {
//                                    if (_Str[_Pos] == 'E') { State = EnumState.x24; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x24:
//                                {
//                                    if (_Str[_Pos] == 'G') { State = EnumState.x25; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x25:
//                                {
//                                    if (_Str[_Pos] == 'E') { State = EnumState.x26; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x26:
//                                {
//                                    if (_Str[_Pos] == 'R') { State = EnumState.x27; }
//                                    else { State = EnumState.Error; SetError(Err.LetterExpected, _Pos); }
//                                    break;
//                                }
//                            case EnumState.x27:
//                                {
//                                    if (_Str[_Pos] == ' ') { State = EnumState.x27; }
//                                    else if (_Str[_Pos] == ';') { State = EnumState.x99; }
//                                    else { State = EnumState.Error; SetError(Err.SpaceDotcommaExpected, _Pos); }
//                                    break;
//                                }
//                            default:
//                                {
//                                    State = EnumState.Error;
//                                    break;
//                                }
//                        }
//                    }

//                    if (State != EnumState.Final) _Pos++;
//                }
//                if (_Pos < _Str.Length - 1) SetError(Err.RightNotAllow, _Pos + 1);
//                return (State == EnumState.Final);
//            }

//            private static void SetError(Err ErrorType, int ErrorPosition)
//            {

//                _Err = ErrorType;
//                _ErrPos = ErrorPosition;

//            }
//        }

//        private void Form1_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}