using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSSearch
{
    /// <summary>
    /// This class defines an object that allows a list of objects to be searched based on one or more string values.
    /// It will return a list of objects that were found to contain one or more of the strings that were being searched for.
    /// The list of objects being searched can contain any object type as long of the type implements the ToString method.
    /// The string being searched for can be passed in as a List of strings or as a single, comma delimited string.
    /// It is also possible to do case-sensitive or case-insensitive searches.
    /// </summary>
    /// <typeparam name="T">Type of object being passed in to search</typeparam>
    public class ToStringSearch<T>
    {
        /// <summary>
        /// Searches the ToString value for each object in a list of objects for occurrances of each string
        /// in a list of string passed in for searching. 
        /// </summary>
        /// <param name="searchTerms">List of string values to find in the list of objects</param>
        /// <param name="listToBeSearched">List of objects to be searched. Type is defined when ToStringSearch object
        /// is instantiated. Object type must implement the ToString method</param>
        /// <returns>List of objects where one or more of the search strings were found. Return null if no matches were
        /// found or either input list was null</returns>
        public List<T> SearchForString(List<string> searchTerms, List<T> listToBeSearched)
        {
            //Search the ToString values of each object in the listToBeSearched for the strings in the searchTerms list
            //Do not enforce case-sensitivity (default)
            return SearchTheList(searchTerms, listToBeSearched, false);
        }

        /// <summary>
        /// Searches the ToString value for each object in a list of objects for occurrances of each string
        /// in a comma delimited list of strings passed in for searching. 
        /// </summary>
        /// <param name="searchTerms">Comma delimited list of string values to find in the list of objects</param>
        /// <param name="listToBeSearched">List of objects to be searched. Type is defined when ToStringSearch object
        /// is instantiated. Object type must implement the ToString method</param>
        /// <returns>List of objects where one or more of the search strings were found. Return null if no matches were
        /// found or either input list was null</returns>
        public List<T> SearchForString(string searchTerms, List<T> listToBeSearched)
        {
            //Convert the comma delimited list to a List<string>
            List<string> srchTerms = new List<string>();
            srchTerms = (searchTerms.Split(',')).ToList();

            //Search the ToString values of each object in the listToBeSearched for the strings in the srchTerms list
            //Do not enforce case-sensitivity (default)
            return SearchTheList(srchTerms, listToBeSearched, false);
        }

        /// <summary>
        /// Searches the ToString value for each object in a list of objects for occurrances of each string
        /// in a list of string passed in for searching. 
        /// </summary>
        /// <param name="searchTerms">List of string values to find in the list of objects</param>
        /// <param name="listToBeSearched">List of objects to be searched. Type is defined when ToStringSearch object
        /// is instantiated. Object type must implement the ToString method</param>
        /// <param name="caseSensitive">True enforces case-sensitivity, false means search is not case-sensitive</param>
        /// <returns>List of objects where one or more of the search strings were found. Return null if no matches were
        /// found or either input list was null</returns>
        public List<T> SearchForString(List<string> searchTerms, List<T> listToBeSearched, bool caseSensitive)
        {
            //Search the ToString values of each object in the listToBeSearched for the strings in the searchTerms list
            //Pass in the desired case-sensitive value
            return SearchTheList(searchTerms, listToBeSearched, caseSensitive);
        }

        /// <summary>
        /// Searches the ToString value for each object in a list of objects for occurrances of each string
        /// in a comma delimited list of strings passed in for searching.
        /// </summary>
        /// <param name="searchTerms">Comma delimited list of string values to find in the list of objects</param>
        /// <param name="listToBeSearched">List of objects to be searched. Type is defined when ToStringSearch object
        /// is instantiated. Object type must implement the ToString method</param>
        /// <param name="caseSensitive">True enforces case-sensitivity, false means search is not case-sensitive</param>
        /// <returns>List of objects where one or more of the search strings were found. Return null if no matches were
        /// found or either input list was null</returns>
        public List<T> SearchForString(string searchTerms, List<T> listToBeSearched, bool caseSensitive)
        {
            //Convert the comma delimited list to a List<string>
            List<string> srchTerms = new List<string>();
            srchTerms = (searchTerms.Split(',')).ToList();

            //Search the ToString values of each object in the listToBeSearched for the strings in the srchTerms list
            //Pass in the desired case-sensitive value
            return SearchTheList(srchTerms, listToBeSearched, caseSensitive);
        }

        /// <summary>
        /// Searches the ToString value of each object in a list of objects for string values passed in as a list of strings.
        /// The objects being searched must implement the ToString method or results may not be as expcted.
        /// </summary>
        /// <param name="searchTerms">List of string values to find in the list of objects</param>
        /// <param name="searchList">List of objects to be searched. Type is defined when ToStringSearch object
        /// is instantiated. Object type must implement the ToString method</param>
        /// <param name="caseSensitive">True enforces case-sensitivity, false means search is not case-sensitive</param>
        /// <returns>List of objects where one or more of the search strings were found. Return null if no matches were
        /// found or either input list was null</returns>
        private List<T> SearchTheList(List<string> searchTerms, List<T> searchList, bool caseSensitive)
        {
            //instantiate the list of return values
            List<T> searchResult = new List<T>();

            //If either of the lists passed in are null then abort the search
            if ((searchTerms != null) && (searchList != null))
            {
                //Check each object in the searchList
                foreach (T item in searchList)
                {
                    //Skip this object is the ToString value is blank
                    if (item.ToString() != "")
                    {
                        //Search for each string in the searchTerms list
                        foreach (string st in searchTerms)
                        {
                            //Do not search for this string if its value is blank
                            if (st != "")
                            {
                                //If the case-sensitive flag is set...
                                if (caseSensitive)
                                {
                                    //...do a case-sensitive search
                                    if (item.ToString().Contains(st))
                                    {
                                        //Match is found. If the object has not already been added to the
                                        //list of search results add this object to the list.
                                        if (!searchResult.Contains(item)) searchResult.Add(item);
                                        //Go to next object in the list - no need to check rest of strings
                                        //since this obj has been added to search results
                                        break;
                                    }
                                }
                                else
                                {
                                    //If the case-sensitive flag is not set do a case-insensitive search
                                    if (item.ToString().IndexOf(st, StringComparison.OrdinalIgnoreCase) >= 0)
                                    {
                                        //Match is found. If the object has not already been added to the
                                        //list of search results add this object to the list.
                                        if (!searchResult.Contains(item)) searchResult.Add(item);
                                        //Go to next object in the list - no need to check rest of strings
                                        //since this obj has been added to search results
                                        break;
                                    }
                                }// end if case-sensitive
                            } //end if st!= ""
                        } //end foreach(string st
                    } //end if ToString()!=0
                } //end foreach item in searchList
            }
            //If no matches were found set return value to null
            if (searchResult.Count == 0) searchResult = null;

            return searchResult;
        }
    }
}

