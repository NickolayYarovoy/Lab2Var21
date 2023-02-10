using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab2V21.DBStructure
{
    /// <summary>
    /// Class with data about flight
    /// </summary>
    public class Info
    {
        /// <value>
        /// Service field for interaction with the database
        /// </value>
        public int? InfoId { get; set; }
        /// <value>
        /// Number of flight
        /// </value>
        public int? FlightNumber { get; set; }
        /// <value>
        /// Aviacompany name
        /// </value>
        public string? AviacompanyName { get; set; }
        /// <value>
        /// Arrival date
        /// </value>
        public DateTime ArrivalDate { get; set; }
        /// <value>
        /// Passenger number
        /// </value>
        public int? PassengerNumber { get; set; }

        /// <summary>
        /// Checks whether the left element is less than the right one or not
        /// </summary>
        /// <returns>
        /// Returns true if left element is less, false else
        /// </returns>
        /// <param name="left">
        /// Left element
        /// </param>
        /// /// <param name="right">
        /// Right element
        /// </param>
        public static bool operator<(Info left, Info right)
        {
            if (left.ArrivalDate == right.ArrivalDate)
            {
                if (left.AviacompanyName == right.AviacompanyName)
                {
                    if (left.PassengerNumber == right.PassengerNumber)
                        return false;
                    else if (left.PassengerNumber > right.PassengerNumber)
                        return true;
                    return false;
                }
                else if (string.Compare(left.AviacompanyName, right.AviacompanyName) < 0)
                    return true;
                return false;
            }
            else if (left.ArrivalDate < right.ArrivalDate)
                return true;

            return false;
        }

        /// <summary>
        /// Checks whether the left element is greater than the right one or not
        /// </summary>
        /// <returns>
        /// Returns true if left element is greater, false else
        /// </returns>
        /// <param name="left">
        /// Left element
        /// </param>
        /// /// <param name="right">
        /// Right element
        /// </param>
        public static bool operator >(Info left, Info right)
        {
            if (left.ArrivalDate == right.ArrivalDate)
            {
                if (left.AviacompanyName == right.AviacompanyName)
                {
                    if (left.PassengerNumber == right.PassengerNumber)
                        return false;
                    else if (left.PassengerNumber < right.PassengerNumber)
                        return true;
                    return false;
                }
                else if (string.Compare(left.AviacompanyName, right.AviacompanyName) > 0)
                    return true;
                return false;
            }
            else if (left.ArrivalDate > right.ArrivalDate)
                return true;

            return false;
        }

        /// <summary>
        /// Checks whether the left element is less or equal than the right one or not
        /// </summary>
        /// <returns>
        /// Returns true if left element is less or equal, false else
        /// </returns>
        /// <param name="left">
        /// Left element
        /// </param>
        /// /// <param name="right">
        /// Right element
        /// </param>
        public static bool operator <=(Info left, Info right)
        {
            if (left.ArrivalDate == right.ArrivalDate)
            {
                if (left.AviacompanyName == right.AviacompanyName)
                {
                    if (left.PassengerNumber == right.PassengerNumber)
                        return true;
                    else if (left.PassengerNumber > right.PassengerNumber)
                        return true;
                    return false;
                }
                else if (string.Compare(left.AviacompanyName, right.AviacompanyName) < 0)
                    return true;
                return false;
            }
            else if (left.ArrivalDate < right.ArrivalDate)
                return true;

            return false;
        }

        /// <summary>
        /// Checks whether the left element is greater or equal than the right one or not
        /// </summary>
        /// <returns>
        /// Returns true if left element is greater or equal, false else
        /// </returns>
        /// <param name="left">
        /// Left element
        /// </param>
        /// /// <param name="right">
        /// Right element
        /// </param>
        public static bool operator >=(Info left, Info right)
        {
            if (left.ArrivalDate == right.ArrivalDate)
            {
                if (left.AviacompanyName == right.AviacompanyName)
                {
                    if (left.PassengerNumber == right.PassengerNumber)
                        return true;
                    else if (left.PassengerNumber < right.PassengerNumber)
                        return true;
                    return false;
                }
                else if (string.Compare(left.AviacompanyName, right.AviacompanyName) > 0)
                    return true;
                return false;
            }
            else if (left.ArrivalDate > right.ArrivalDate)
                return true;

            return false;
        }
    }
}
