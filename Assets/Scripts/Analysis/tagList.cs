using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
//This allows the IComparable Interface

//This is the class you will be storing
//in the different collections. In order to use
//a collection's Sort() method, this class needs to
//implement the IComparable interface.
public class TagList : IComparable<TagList>
{

    public string tag;
	public int type;

	//public int number;

	public TagList(string newTag, int newType)
	{
		tag = newTag;
		type = newType;
		//number = newNumber;
	}

	//This method is required by the IComparable
	//interface. 
	public int CompareTo(TagList other)
	{
		if(other == null)
		{
			return 1;
		}

        //Return the difference in power.
        return tag.CompareTo(other.tag);
    }

}

public class Tags : IComparable<Tags>
{
	public string name;

	//public int number;

	public Tags(string newTag)
	{
		name = newTag;

		//number = newNumber;
	}

	//This method is required by the IComparable
	//interface. 
	public int CompareTo(Tags other)
	{
		if(other == null)
		{
			return 1;
		}

		//Return the difference in power.
		return name.CompareTo (other.name);
	}

}

public class fCheckedTag : IComparable<fCheckedTag>
{
    public string name;

    //public int number;

    public fCheckedTag(string newTag)
    {
        name = newTag;
    }

    //This method is required by the IComparable
    //interface. 
    public int CompareTo(fCheckedTag other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return name.CompareTo(other.name);
    }

}

public class fCheckedParticipant : IComparable<fCheckedParticipant>
{
    public string participantName;

    //public int number;

    public fCheckedParticipant(string name)
    {
        participantName = name;
    }

    //This method is required by the IComparable
    //interface. 
    public int CompareTo(fCheckedParticipant other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return participantName.CompareTo(other.participantName);
    }

}

public class Check: IComparable<Check>
{
    public string tagID;

    //public int number;

    public Check(string newID)
    {
        tagID = newID;

        //number = newNumber;
    }

    //This method is required by the IComparable
    //interface. 
    public int CompareTo(Check other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return tagID.CompareTo(other.tagID);
    }

}

public class FilteredTags : IComparable<FilteredTags>
{
    public string taskName;
    public string participantName;




    public FilteredTags(string task, string participant)
    {
        taskName = task;
        participantName = participant;



        //number = newNumber;
    }

    //This method is required by the IComparable
    //interface. 
    public int CompareTo(FilteredTags other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return taskName.CompareTo(other.taskName);
    }

}


public class CheckedTags : IComparable<CheckedTags>
{
    public string tag;

    public CheckedTags(string tagName)
    {
        tag = tagName;

        //number = newNumber;
    }

    //This method is required by the IComparable
    //interface. 
    public int CompareTo(CheckedTags other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return tag.CompareTo(other.tag);
    }

}

public class Tasks : IComparable<Tasks>
{
    public string task;
    public List<CheckedTags> checkedTags = new List<CheckedTags>();


    public Tasks (string taskName, List<CheckedTags> tagName)
    {
        task = taskName;
        checkedTags = tagName;

        //number = newNumber;
    }

    //This method is required by the IComparable
    //interface. 
    public int CompareTo(Tasks other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return task.CompareTo(other.task);
    }

}

public class Participants : IComparable<Participants>
{
    public string participant;
    public List<Tasks> tasks = new List<Tasks>();

    public Participants (string participantName, List<Tasks> taskName)
    {
        participant = participantName;
        tasks = taskName;

        //number = newNumber;
    }

    //This method is required by the IComparable
    //interface. 
    public int CompareTo(Participants other)
    {
        if (other == null)
        {
            return 1;
        }

        //Return the difference in power.
        return participant.CompareTo(other.participant);
    }

}