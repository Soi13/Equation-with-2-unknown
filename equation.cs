using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
					
public class Program
{
	public static void Main()
	{		
		string equation;
		
		Console.WriteLine("Input equation in format like (4a-3=2a+7):");
		equation=Console.ReadLine();
		
		if (equation.Length==0)
		{
			Console.WriteLine("\nYou didn't input equation!");
			return;
		}
		
		if (!equation.Contains("="))
		{
			Console.WriteLine("\nIncorrect input of equation! Format must contain sign \"=\" and be like 4a-3=2a+7");
			return;
		}
		
		List<string> left_part_equation = new List<string>();
		List<string> right_part_equation = new List<string>();
	    String[] devided_equation = new String[2];
		devided_equation=equation.Split('=');
		string temp_matches1="";
		string temp_matches2="";
		int sum_left=0, sum_right=0, tmp_sum_left=0, tmp_sum_right=0, res=0;
		string temp_el="";
						
		Regex rgx= new Regex(@"(([a-z]+\d+)|([-+*/]\d+[a-z]+|\d+[a-z]+))[a-z\d]*", RegexOptions.IgnoreCase); //Get all variables
		Regex rgx1= new Regex(@"[-+*/]?\d+\b", RegexOptions.IgnoreCase); //Get all digits
		Regex rgx2= new Regex(@"[a-zA-Z]", RegexOptions.IgnoreCase); //Get letter which used as a variable
				
		//Detect letter used in equation
		Match m = rgx2.Match(devided_equation[0]);
		string letter=m.Value;
		///////////////////////////////
		
		//Move variables to left part of equation 
		foreach (Match matches in rgx.Matches(devided_equation[0]))
		{
		    left_part_equation.Add(matches.Value.Replace("+", ""));				
		}
		
		foreach (Match matches1 in rgx.Matches(devided_equation[1]))
		{
		    temp_matches1=matches1.Value;						
			if (temp_matches1[0].ToString()=="+")
			{ 
				temp_matches1=temp_matches1.Replace("+", "-");
				left_part_equation.Add(temp_matches1);
				continue;
			}			
			else if (temp_matches1[0].ToString()!="-")
			{ 
				temp_matches1="-"+temp_matches1;
				left_part_equation.Add(temp_matches1);
				continue;
			}			
			else if (temp_matches1[0].ToString()=="-")
			{
				temp_matches1=temp_matches1.Replace("-", "");
				left_part_equation.Add(temp_matches1);
				continue;
			}
			else 
			{
				left_part_equation.Add(temp_matches1);
				continue;
			}			
			
		}	
		//////////////////////////////////////////
		
		Console.WriteLine("\nFor solution we devide equation for two parts, in the left part we move all variables and in the right part we move numbers. \nIn process of moving variables or numbers we change their sign in opposite.\n");
		//Output variables of left part of equation
		foreach (string k in left_part_equation)
		{
			Console.Write("{0}",k);
		}
		Console.Write("=");
		
		
		//Move digits to right part of equation
		foreach (Match matches2 in rgx1.Matches(devided_equation[0]))
		{
			temp_matches2=matches2.Value;
			if (temp_matches2[0].ToString()=="+")
			{ 
				temp_matches2=temp_matches2.Replace("+", "-");
				right_part_equation.Add(temp_matches2);
				continue;
			}
			else if (temp_matches2[0].ToString()!="-")
			{ 
				temp_matches2="-"+temp_matches2;
				right_part_equation.Add(temp_matches2);
				continue;
			}			
			else if (temp_matches2[0].ToString()=="-")
			{
				temp_matches2=temp_matches2.Replace("-", "");
				right_part_equation.Add(temp_matches2);
				continue;
			}
			else 
			{
				right_part_equation.Add(temp_matches2);
				continue;
			}								
		}
						
		foreach (Match matches3 in rgx1.Matches(devided_equation[1]))
		{
		    right_part_equation.Add(matches3.Value.Replace("+", ""));				
		}		
		////////////////////////////////////////////
		
		//Output digits of right part of equation
		for (int k1=0; k1<=right_part_equation.Count-1; k1++)
		{
			if (k1>=1) 
			{ 
				right_part_equation[k1]="+"+right_part_equation[k1];
			}
			Console.Write("{0}",right_part_equation[k1]);
		}
		
		
		//Summation of variables in left part of equation
		for (int v=0; v<=left_part_equation.Count-1; v++)			
		{
			temp_el=left_part_equation[v];
			temp_el=temp_el.TrimEnd(temp_el[temp_el.Length-1]);
			tmp_sum_left=Int32.Parse(temp_el);
			sum_left+=tmp_sum_left;			
		}
		
		//Summation of digits in right part of equation
		for (int v1=0; v1<=right_part_equation.Count-1; v1++)			
		{
			tmp_sum_right=Int32.Parse(right_part_equation[v1]);
			sum_right+=tmp_sum_right;			
		}
		
		Console.WriteLine("\n\nthen we are simplify equation:\n");
		Console.WriteLine("{0}{1}{2}{3}", sum_left, letter,"=", sum_right);
		
		//Output result
		res=sum_right/sum_left;			
		Console.WriteLine("\nResult: {0}{1}{2}{3}{4}{5}{6}",letter,"=",sum_right,"/",sum_left, "=", res);	
		
	}
}