using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BlImplementation
{
    /// <summary>
    /// Implements project-related tools and utilities.
    /// Interacts with the DAL for project data access and manipulation.
    /// </summary>
    internal class ToolsImplementation : BlApi.ITools
    {
        /// <summary>
        /// DAL instance for data access.
        /// </summary>
        private DalApi.IDal _dal = DalApi.Factory.Get;

        /// <summary>
        /// Gets the project's end date.
        /// </summary>
        /// <returns>The project's end date, or null if not set.</returns>
        public DateTime? getProjectEndDate()
        {
            return _dal.getProjectEndDate();
        }

        /// <summary>
        /// Gets the project's start date.
        /// </summary>
        /// <returns>The project's start date, or null if not set.</returns>
        public DateTime? getProjectStartDate()
        {
            return _dal.getProjectStartDate();
        }

        /// <summary>
        /// Sets the project's end date.
        /// </summary>
        /// <param name="endDate">The new end date to set.</param>
        public void SetProjectEndDate(DateTime? endDate)
        {
            _dal.SetProjectEndDate(endDate);
        }

        /// <summary>
        /// Sets the project's start date.
        /// </summary>
        /// <param name="startDate">The new start date to set.</param>
        public void SetProjectStartDate(DateTime? startDate)
        {
            _dal.SetProjectStartDate(startDate);
        }
    }
}
