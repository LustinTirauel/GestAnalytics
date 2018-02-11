using UnityEngine;
using System.Collections;
using System; //This allows the IComparable Interface

//This is the class you will be storing
//in the different collections. In order to use
//a collection's Sort() method, this class needs to
//implement the IComparable interface.
public class Answers : IComparable<Answers>
{
	public string name;
	public int number;
	
	public Answers(string newName, int newNumber)
	{
		name = newName;
		number = newNumber;
	}
	
	//This method is required by the IComparable
	//interface. 
	public int CompareTo(Answers other)
	{
		if(other == null)
		{
			return 1;
		}
		
		//Return the difference in power.
		return name.CompareTo (other.name);;
	}

}

public class CollectedAnswers : IComparable<CollectedAnswers>

{

	public string input;

	public CollectedAnswers(string newInput)
	{
		input = newInput;
	}


	public int CompareTo(CollectedAnswers other)
	{
		if(other == null)
		{
			return 1;
		}

		//Return the difference in power.
		return input.CompareTo (other.input);;
	}
}