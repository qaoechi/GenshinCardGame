using UnityEngine;
using System;
using System.Data;

namespace Assets.Script
{
    public class Calculator
    {
        public static string strFormula;
        public static string DeriveFormula()
        {
            try
            {
                if (IsValidFormula())
                {
                    strFormula = EvalExpression().ToString();
                    return strFormula;
                }
                else
                {
                    return null;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.Log(e);
                return null;

            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.Log(e);
                return null;
            }
        }
        public static void DeleteFormula()
        {
            if (strFormula == null && strFormula =="")
            {
                Debug.Log("식이 없음!");
                return;
            }

            try
            {
                strFormula = strFormula.Substring(0, strFormula.Length - 1);
                ButtonClick.tmpFormula.text = strFormula;
            } catch (IndexOutOfRangeException e)
            {
                Debug.Log(e);
            } catch (ArgumentOutOfRangeException e)
            {
                Debug.Log(e);
            }

        }
        public static void ResetFormula()
        {
            strFormula = null;
            ButtonClick.tmpFormula.text = strFormula;
        }
        public static bool IsValidFormula()
        {
            //ResetLists();
            if (strFormula == null)
            {
                Debug.Log("식이 없음");
                return false;
            }
            if (IsValidOperator(strFormula[0]))
            {
                Debug.Log("시작이 연산자임");
                return false;
            }
            /*foreach (char c in strFormula)
            {
                if (IsValidOperator(c))
                {
                    break;
                }
                Debug.Log("수밖에 없음");
                return false;
            }*/
            if (ContainsConsecutiveOperators())
            {
                Debug.Log("연속된 연산자");
                return false;
            }
            return true;
        }
        public static bool ContainsConsecutiveOperators()
        {
            for (int i = 0; i < strFormula.Length - 1; i++)
            {
                if (IsValidOperator(strFormula[^1]))
                {
                    Debug.Log("마지막 문자가 연산자임");
                    return true;
                }
                char current = strFormula[i];
                char next = strFormula[i + 1];
                if (IsValidOperator(current) && IsValidOperator(next))
                {
                    Debug.Log("연속된 연산자");
                    return true;
                }
            }
            return false;
        }
        private static bool IsValidOperator(char c)
        {
            return c == '+' || c == '-' || c == '×';
        }
        private static string multiplyOp()
        {
            string exp = null;
            char c;
            for (int i = 0; i < strFormula.Length; i++)
            {
                c = strFormula[i];
                if (c == '×')
                {
                    c = '*';
                }
                exp += c;
            }
            return exp;
        }
        private static double EvalExpression()
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("expression", typeof(double), multiplyOp());
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                return (double)row["expression"];
            } catch (SyntaxErrorException e)
            {
                Debug.LogError(e);
                return 0;
            }
        }
    }
}
