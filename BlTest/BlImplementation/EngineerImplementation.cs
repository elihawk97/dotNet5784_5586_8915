using BlApi;
using BO;
using System; 


namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

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
            boItem.Cost) ;

        try {

            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }
        
        catch (DO.DalAlreadyExistsException ex) {
            throw new Exception(
            $"Engineer with ID={boItem.Id} already exists", ex
            );
        }
    }

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
                            (ExperienceLevel?)doEngineer.EngineerExperience, 
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
                    (ExperienceLevel?)doEngineer.EngineerExperience,
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
                    (BO.ExperienceLevel?)DOengineer.EngineerExperience,
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
