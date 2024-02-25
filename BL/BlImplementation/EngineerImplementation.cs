using BlApi;
using BO;
using System;


namespace BlImplementation;

/// <summary>
/// Class implementing the IEngineer interface to manipulate the
/// Engineer list object of the Bl
/// </summary>
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Creates a new engineer record in the system.
    /// Validates the provided boItem for mandatory fields and data integrity.
    /// Throws exceptions for invalid data or duplicate engineer IDs.
    /// Returns the ID of the newly created engineer.
    /// </summary>
    /// <param name="boItem">The engineer data to be created.</param>
    /// <returns>The ID of the newly created engineer.</returns>
    public int Create(BO.Engineer boItem)
    {
        if (boItem.Id <= 0)
        {
            throw new Exception("Invalid identifier. Must be a positive value.");
        }

        // Check for engineer's name (non-empty string)
        if (string.IsNullOrEmpty(boItem.Name))
        {
            throw new Exception("Engineer's name cannot be empty.");
        }

        if (boItem.Cost <= 0)
        {
            throw new Exception("Invalid cost. Must be a positive value.");
        }

        if (!IsValidEmail(boItem.Email))
        {
            throw new Exception("Invalid email address pattern.");
        }

        DO.Engineer doEngineer = new DO.Engineer(
            boItem.Id,
            boItem.Name,
            boItem.Email,
            (DO.Enums.ExperienceLevel?)boItem.Level,
            boItem.Cost);

        try
        {

            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }

        catch (DO.DalAlreadyExistsException ex)
        {
            throw new Exception(
            $"Engineer with ID={boItem.Id} already exists", ex
            );
        }
    }

    /// <summary>
    /// Deletes an engineer record with the specified ID.
    /// Throws an exception if the engineer with the given ID does not exist.
    /// </summary>
    /// <param name="id">The ID of the engineer to delete.</param>
    public void DeleteEngineer(int id)
    {
        try
        {
            _dal.Engineer.Delete(_dal.Engineer.Read(id).Id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new Exception(
            $"Engineer with ID={id} does not exist", ex
            );

        }
    }
    /// <summary>
    /// Retrieves all engineer records from the system.
    /// Optionally applies a provided filter function to the results.
    /// Maps retrieved DO.Engineer objects to BO.Engineer objects.
    /// Throws an exception if an error occurs while reading engineers.
    /// </summary>
    /// <param name="filter">Optional filter function to apply to the results.</param>
    /// <returns>An IEnumerable of BO.Engineer objects representing the retrieved engineers.</returns>

    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool> filter)
    {
        try
        {
            IEnumerable<DO.Engineer> doEngineers;


            // If a filter is provided, use DAL's filtering capability
            if (filter != null)
            {
                doEngineers = _dal.Engineer.ReadAll(doEngineer =>
                    filter(new BO.Engineer(
                            doEngineer.Id,
                            doEngineer.Name,
                            doEngineer.Email,
                            (Enums.ExperienceLevel?)doEngineer.EngineerExperience,
                            doEngineer.Cost
                            )))!;
            }
            else
            {
                // Otherwise, fetch all records
                doEngineers = _dal.Engineer.ReadAll();
            }

            // Map DO.Engineer objects to BO.Engineer objects
            IEnumerable<BO.Engineer> boEngineers = doEngineers.Select(doEngineer =>
                new BO.Engineer
                (
                    doEngineer.Id,
                    doEngineer.Name,
                    doEngineer.Email,
                    (Enums.ExperienceLevel?)doEngineer.EngineerExperience,
                    doEngineer.Cost
                ));

            return boEngineers;
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new Exception(
                $"Error occurred while reading engineers", ex
            );
        }
    }

/// <summary>
    /// Retrieves an engineer record with the specified ID.
    /// Returns the engineer as a BO.Engineer object if found, otherwise returns null.
    /// Throws an exception if the engineer with the given ID does not exist.
    /// </summary>
    /// <param name="id">The ID of the engineer to retrieve.</param>
    /// <returns>The retrieved engineer as a BO.Engineer object, or null if not found.</returns>

    public Engineer? ReadEngineer(int id)
    {

        try
        {
            DO.Engineer? DOengineer = _dal.Engineer.Read(id);
            BO.Engineer? BOengineerToRead = null;


            if (DOengineer != null)
            {
                BOengineerToRead = new BO.Engineer(
                   DOengineer.Id,
                   DOengineer.Name,
                   DOengineer.Email,
                   (BO.Enums.ExperienceLevel?)DOengineer.EngineerExperience,
                   DOengineer.Cost
                   );

            }
            return BOengineerToRead;
        }

        catch (DO.DalDoesNotExistException ex)
        {
            throw new Exception(
                $"Engineer with {id} does not exist", ex
            );
        }

    }

    /// <summary>
    /// Updates an existing engineer record with the provided details.
    /// Validates the provided boItem for mandatory fields and data integrity.
    /// Throws an exception if the engineer with the given ID does not exist.
    /// </summary>
    /// <param name="boItem">The updated engineer data.</param>
    public void UpdateEngineer(Engineer boItem)
    {
        try
        {
            DO.Engineer? DOengineer = _dal.Engineer.Read(boItem.Id);

            if (DOengineer != null)
            {
                DOengineer = new DO.Engineer(
                boItem.Id,
                boItem.Name,
                boItem.Email,
                (DO.Enums.ExperienceLevel?)boItem.Level,
                boItem.Cost);

                _dal.Engineer.Update(DOengineer);
            }

        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new Exception(
                $"Engineer with {boItem.Id} does not exist", ex
            );
        }

    }

    /// <summary>
    /// Resets the engineer data clears the data source.
    /// </summary>
    public void Reset()
    {
        _dal.Engineer.Reset();
    }

    /// <summary>
    /// Validates the format of an email address.
    /// Checks for the presence and position of the '@' symbol.
    /// Checks for a valid domain ending (limited to specific domains in this example).
    /// Returns true if the email format is valid, false otherwise.
    /// </summary>
    /// <param name="email">The email address to validate.</param>
    /// <returns>True if the email format is valid, false otherwise.</returns>
    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }

        // Check for the presence of '@'
        int atIndex = email.IndexOf('@');
        if (atIndex == -1 || atIndex == 0 || atIndex == email.Length - 1)
        {
            return false;
        }

        // Check for a valid domain ending
        string domain = email.Substring(atIndex + 1);
        if (domain.EndsWith(".co.il") || domain.EndsWith(".org") || domain.EndsWith(".com"))
        {
            return true;
        }

        return false;
    }

}