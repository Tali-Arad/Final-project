using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourGuideProtocol.DataStruct;
using TourGuideProtocol.DataInt;


namespace TourGuideDAL
{
    public class TourGuideOp : ITourOp
    {
        public List<ATour> GetTours() // gets all tours
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<ATour> rows = (from c in dc.Tours
                                    select new ATour()
                                    {
                                        TourID = c.TourID.ToString(),
                                        TourName = c.TourName,
                                        TourLocation = c.TourLocation,
                                        TourArea = c.TourArea,
                                        TourCategory = c.TourCategory,
                                        TourDuration = (int)c.TourDuration,
                                        TourPrice = (decimal)c.TourPrice,
                                        MinReg = (int)c.MinReg,
                                        MaxReg = (int)c.MaxReg,
                                        TourDescription = c.TourDescription,
                                        ImageData = c.ImageData,
                                        ImageMimeType = c.ImageMimeType
                                    }
                              ).ToList<ATour>();
                return rows;
            }
        }

        public List<ATour> GetTours(string keyword) // gets tours by location/area/category
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<ATour> rows = (from c in dc.Tours
                                    where c.TourLocation.Contains(keyword) || c.TourArea.Contains(keyword) || c.TourCategory.Contains(keyword)
                                    select new ATour()
                                     {
                                         TourID= c.TourID.ToString(),
                                         TourName = c.TourName,
                                         TourLocation = c.TourLocation,
                                         TourArea = c.TourArea,
                                         TourCategory = c.TourCategory,
                                         TourDuration = (int)c.TourDuration, 
                                         TourPrice = (decimal)c.TourPrice,
                                         MinReg = (int)c.MinReg,
                                         MaxReg = (int)c.MaxReg,
                                         TourDescription = c.TourDescription,
                                         ImageData = c.ImageData,
                                         ImageMimeType = c.ImageMimeType
                                     }
                              ).ToList<ATour>();
                return rows;       
            }
        }


        public List<AEvent> GetEvents() // gets all events
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<AEvent> rows = (from tours in dc.Tours
                                     join events in dc.Events on tours.TourID equals events.TourID
                                     orderby events.TourDate
                                     select new AEvent()
                                     {
                                         TourName = tours.TourName, 
                                         TourID = events.TourID.ToString(),
                                         TourDate = events.TourDate,
                                         TourOriginalDate = events.TourDate,
                                         TourGuide = events.TourGuide,
                                         IsOn =  (events.IsOn==1) ? true:false
                                     }
                              ).ToList<AEvent>();
                return rows;
            }
        }

        public List<AEvent> GetEvents(string tourName) // gets events for specific tour
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<AEvent> rows = (from tours in dc.Tours
                                     where tours.TourName == tourName
                                     join events in dc.Events on tours.TourID equals events.TourID
                                     orderby events.TourDate
                                     select new AEvent()
                                     {
                                         TourName = tours.TourName,
                                         TourID = events.TourID.ToString(),
                                         TourDate = events.TourDate,
                                         TourOriginalDate = events.TourDate,
                                         TourGuide = events.TourGuide,
                                         IsOn = events.IsOn==1? true : false
                                     }
                              ).ToList<AEvent>();
                return rows;
            }
        }

        public List<AEvent> GetEventsByTourField(string sort) // gets events with sorting by location, area, category 
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<AEvent> rows = (from tours in dc.Tours
                                     where tours.TourLocation == sort || tours.TourArea == sort || tours.TourCategory == sort
                                     join events in dc.Events on tours.TourID equals events.TourID
                                     orderby events.TourDate
                                     select new AEvent()
                                     {
                                         TourName = tours.TourName,
                                         TourID = events.TourID.ToString(),
                                         TourDate = events.TourDate,
                                         TourOriginalDate = events.TourDate,
                                         TourGuide = events.TourGuide,
                                         IsOn = events.IsOn==1 ? true:false
                                     }
                              ).ToList<AEvent>();
                return rows;
            }
        }

        public List<AEvent> GetEventsByEventField(string sort) // gets events with sorting by guide or date
        {
            List<string> months = new List<string>{"January", "February", "March", "April", "May", "June",
                                  "July", "August", "September", "October", "November", "December"};
            int month = 0;
            if(months.Contains(sort))
            { 
                  month = DateTime.Parse("1." + sort + " 1900").Month;
            }
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<AEvent> rows = (from events in dc.Events
                                     where events.TourGuide == sort || events.TourDate.Month == month
                                     join tours in dc.Tours on events.TourID equals tours.TourID
                                     orderby events.TourDate
                                     select new AEvent()
                                     {
                                         TourName = tours.TourName,
                                         TourID = events.TourID.ToString(),
                                         TourDate = events.TourDate,
                                         TourOriginalDate = events.TourDate,
                                         TourGuide = events.TourGuide,
                                         IsOn = events.IsOn==1? true:false
                                     }
                              ).ToList<AEvent>();
                return rows;
            }
        }

        public List<AEvent> GetEventsByTourId(string id)
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<AEvent> rows = (from tours in dc.Tours
                                     where tours.TourID.ToString() == id
                                     join events in dc.Events on tours.TourID equals events.TourID
                                     orderby events.TourDate
                                     select new AEvent()
                                     {
                                         TourName = tours.TourName,
                                         TourID = events.TourID.ToString(),
                                         TourDate = events.TourDate,
                                         TourOriginalDate = events.TourDate,
                                         TourGuide = events.TourGuide,
                                         IsOn = events.IsOn==1?true:false
                                     }
                              ).ToList<AEvent>();
                return rows;
            }
        }

        public List<AEvent> GetEvents(DateTime date) // gets events by date
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<AEvent> rows = (from c in dc.Events
                                    where c.TourDate == date
                                    select new AEvent()
                                    {
                                        TourID = c.TourID.ToString(),
                                        TourDate = c.TourDate,
                                        TourGuide = c.TourGuide,
                                        IsOn = c.IsOn==1?true:false
                                    }
                              ).ToList<AEvent>();
                return rows;
            }
        }

        public AEvent GetEvent(string id, DateTime date) // gets event by tour id and tour date
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                AEvent tourEvent = (from tours in dc.Tours
                                    join events in dc.Events on tours.TourID equals events.TourID
                                    where events.TourDate == date && events.TourID.ToString() == id
                                    select new AEvent()
                                    {
                                        TourName = tours.TourName,
                                        TourID = events.TourID.ToString(),
                                        TourDate = events.TourDate,
                                        TourOriginalDate = events.TourDate,
                                        TourGuide = events.TourGuide,
                                        IsOn = events.IsOn==1?true:false
                                    }
                              ).Single<AEvent>();
                return tourEvent;
            }
        }

        public ATour GetTourByID(string tourID) // gets tour details by tour id
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                ATour tour = (from c in dc.Tours
                              where c.TourID.ToString() == tourID
                              select new ATour()
                              {
                                  TourID = c.TourID.ToString(),
                                  TourName = c.TourName,
                                  TourLocation = c.TourLocation,
                                  TourArea = c.TourArea,
                                  TourCategory = c.TourCategory,
                                  TourDuration = (int)c.TourDuration,
                                  TourPrice = (decimal)c.TourPrice,
                                  MinReg = (int)c.MinReg,
                                  MaxReg = (int)c.MaxReg,
                                  TourDescription = c.TourDescription,
                                  ImageData = c.ImageData,
                                  ImageMimeType = c.ImageMimeType
                              }
                              ).FirstOrDefault<ATour>();
                return tour;
            }
        }

        public List<AReg> GetRegistrations() // gets all registrations
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<AReg> rows = (from users in dc.Users
                                   join regs in dc.Registrations on users.UserID equals regs.UserID
                                   join tours in dc.Tours on regs.TourID equals tours.TourID
                                   orderby regs.TourDate
                                   select new AReg()
                                     {
                                         RegID = regs.RegID.ToString(),
                                         TourID = regs.TourID.ToString(),
                                         TourName = tours.TourName,
                                         TourDate = regs.TourDate,
                                         UserID = regs.UserID.ToString(),
                                         UserName = users.Username,
                                         RegFirstName = regs.RegFirstName,
                                         RegLastName = regs.RegLastName,
                                         RegBirthday = regs.RegBirthday,
                                         WillAttend = regs.WillAttend==1?true:false,
                                         IsPaid = regs.IsPaid==1? true:false,
                                         IsSentEmail = regs.IsSentEmail==1?true:false,
                                         Attended = regs.Attended==1?true:false,
                                         RegTime = regs.RegTime   
                                     }
                              ).ToList<AReg>();
                return rows;
            }
        }

        public List<AReg> GetRegistrationsByUserID(string userID) // gets registrations related to user
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<AReg> rows = (from tours in dc.Tours 
                                   join regs in dc.Registrations on tours.TourID equals regs.TourID
                                   where regs.UserID.ToString()==userID
                                   orderby regs.TourDate
                                   select new AReg()
                                   {
                                       RegID = regs.RegID.ToString(),
                                       TourID = regs.TourID.ToString(),
                                       TourName = tours.TourName,
                                       TourDate = regs.TourDate,
                                       UserID = regs.UserID.ToString(),
                                       UserName = "",
                                       RegFirstName = regs.RegFirstName,
                                       RegLastName = regs.RegLastName,
                                       RegBirthday = regs.RegBirthday,
                                       WillAttend = regs.WillAttend == 1 ? true : false,
                                       IsPaid = regs.IsPaid == 1 ? true : false,
                                       IsSentEmail = regs.IsSentEmail == 1 ? true : false,
                                       Attended = regs.Attended == 1 ? true : false,
                                       RegTime = regs.RegTime
                                   }
                              ).ToList<AReg>();
                return rows;
            }
        }

     
        public List<AUser> GetUsers() // gets all users
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<AUser> rows = (from c in dc.Users
                                    orderby c.RegTime
                                    select new AUser()
                                    {
                                        UserID = c.UserID.ToString(),
                                        RegTime = c.RegTime,
                                        UserIP = c.RegIP,
                                        UserFirstName = c.UserFirstName,
                                        UserLastName = c.UserLastName,
                                        UserPhone = c.UserPhone,
                                        UserEmail = c.UserEmail,
                                        UserPassword = c.UserPassword,
                                        Username = c.Username,
                                        UserBirthday = c.UserBirthday,
                                    }
                              ).ToList<AUser>();
                return rows;
            }
        }

        public AUser GetUser(string username) // gets user by username
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                AUser user = (from c in dc.Users
                                where c.Username == username
                                    select new AUser()
                                    {
                                        UserID = c.UserID.ToString(),
                                        RegTime = c.RegTime,
                                        UserIP = c.RegIP,
                                        UserFirstName = c.UserFirstName,
                                        UserLastName = c.UserLastName,
                                        UserPhone = c.UserPhone,
                                        UserEmail = c.UserEmail,
                                        UserPassword = c.UserPassword,
                                        Username = c.Username,
                                        UserBirthday = c.UserBirthday,
                                    }
                              ).SingleOrDefault<AUser>();
                return user;
            }
        }

        public bool AddReg(AReg reg)
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                Registration registration = new Registration();
                registration.IsSentEmail = 0;
                registration.IsPaid = 0;
                registration.Attended = 0;
                registration.RegFirstName = reg.RegFirstName;
                registration.RegLastName = reg.RegLastName;
                registration.RegBirthday = reg.RegBirthday;
                registration.TourDate = reg.TourDate;
                registration.TourID = System.Guid.Parse(reg.TourID);
                registration.UserID = System.Guid.Parse(reg.UserID);
                registration.RegTime = DateTime.Now;
                registration.WillAttend = (byte)(reg.WillAttend?1:0);
                registration.RegID = System.Guid.NewGuid();
                dc.Registrations.InsertOnSubmit(registration);
                dc.SubmitChanges();
                return true;
            }
        }

        public bool AddUser(AUser userReg)
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                User user = new User();
                user.RegTime = (DateTime)userReg.RegTime;
                user.RegIP = userReg.UserIP;
                user.UserFirstName = userReg.UserFirstName;
                user.UserLastName = userReg.UserLastName;
                user.UserPhone = userReg.UserPhone;
                user.UserEmail = userReg.UserEmail;
                user.UserPassword = userReg.UserPassword;
                user.UserBirthday = userReg.UserBirthday;
                user.Username = userReg.Username;
                user.UserID = System.Guid.NewGuid(); 

                dc.Users.InsertOnSubmit(user);
                dc.SubmitChanges();
                return true;
            }
        }

        public bool AddTour(ATour tour)
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                Tour newTour = new Tour();
                newTour.TourName = tour.TourName;
                newTour.TourPrice = tour.TourPrice;
                newTour.TourLocation = tour.TourLocation;
                newTour.TourArea = tour.TourArea;
                newTour.TourCategory = tour.TourCategory;
                newTour.TourDuration = (short)tour.TourDuration;
                newTour.TourDescription = tour.TourDescription;
                newTour.MinReg = (byte)tour.MinReg;
                newTour.MaxReg = (byte)tour.MaxReg;
                newTour.TourID = System.Guid.NewGuid(); 
                dc.Tours.InsertOnSubmit(newTour);
                dc.SubmitChanges();
                return true;
            }
        }

        public bool AddEvent(AEvent tourEvent)
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                Event newEvent = new Event();
                newEvent.TourID = System.Guid.Parse(tourEvent.TourID);
                newEvent.TourDate = tourEvent.TourDate;
                newEvent.TourGuide = tourEvent.TourGuide;
                newEvent.IsOn = (byte)(tourEvent.IsOn? 1 : 0);
                dc.Events.InsertOnSubmit(newEvent);
                dc.SubmitChanges();
                return true;
            }
        }


        public AUser GetUserByUsername(string username) // gets user by username
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                AUser user = (from c in dc.Users
                                    where c.Username == username
                                    select new AUser()
                                    {
                                        UserID = c.UserID.ToString(),
                                        RegTime = c.RegTime,
                                        UserIP = c.RegIP,
                                        UserFirstName = c.UserFirstName,
                                        UserLastName = c.UserLastName,
                                        UserPhone = c.UserPhone,
                                        UserEmail = c.UserEmail,
                                        UserPassword = c.UserPassword,
                                        Username = c.Username,
                                        UserBirthday = c.UserBirthday,
                                    }
                              ).FirstOrDefault<AUser>();
                return user;
            }
        }

        public bool EditEvent(AEvent tourEvent)
        {
        using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
        {
            if (tourEvent.TourDate == tourEvent.TourOriginalDate) // The TourDate is not changed - save the other changes
            {
                    Event row = (from c in dc.Events
                                 where (c.TourID.ToString() == tourEvent.TourID && c.TourDate == tourEvent.TourDate)
                                 select c).FirstOrDefault<Event>();
                    row.TourGuide = tourEvent.TourGuide;
                row.IsOn = (byte)(tourEvent.IsOn?1:0);
                    dc.SubmitChanges();
                    return true;
            }
            else // The TourDate has been changed - delete the event and create a new one with the new date. Copy the registrations with the new date
            {
                    AddEvent(tourEvent); // Adding the new event (the same event with the new date)
                    // Adding new registrations to the new event (with the new date)
                    List<Registration> registrations = (from r in dc.Registrations
                                                        where (r.TourID.ToString() == tourEvent.TourID && r.TourDate == tourEvent.TourOriginalDate)
                                                        select r).ToList();
                    foreach (Registration reg in registrations)
                    {
                        AReg newReg = new AReg();
                        newReg.IsSentEmail = reg.IsSentEmail==1?true:false;
                        newReg.IsPaid = reg.IsPaid==1?true:false;
                        newReg.Attended = reg.Attended==1?true:false;
                        newReg.RegFirstName = reg.RegFirstName;
                        newReg.RegLastName = reg.RegLastName;
                        newReg.RegBirthday = reg.RegBirthday;
                        newReg.TourDate = tourEvent.TourDate;
                        newReg.TourID = tourEvent.TourID;
                        newReg.UserID = reg.UserID.ToString();
                        newReg.WillAttend = reg.WillAttend==1?true:false;
                        AddReg(newReg);
                    }
                    DeleteEvent(tourEvent.TourID, tourEvent.TourOriginalDate); // this function also deletes the previous date regisrations
                    dc.SubmitChanges();
                    return true;  
              }
        }
                    
        }

        public bool EditReg(AReg reg)
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {

                Registration row = (from c in dc.Registrations
                             where (c.RegID.ToString() == reg.RegID)
                             select c).FirstOrDefault<Registration>();
                row.IsSentEmail = (byte)(reg.IsSentEmail?1:0);
                row.IsPaid = (byte)(reg.IsPaid?1:0);
                row.Attended = (byte)(reg.Attended?1:0);
                row.RegFirstName = reg.RegFirstName;
                row.RegLastName = reg.RegLastName;
                row.RegBirthday = reg.RegBirthday;
                row.WillAttend = (byte)(reg.WillAttend?1:0);
                dc.SubmitChanges();
                return true;     
            }
        }

        public bool EditTour(ATour tour)
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {

                Tour row = (from c in dc.Tours
                                    where (c.TourID.ToString() == tour.TourID)
                                    select c).FirstOrDefault<Tour>();
                row.MaxReg = (byte)tour.MaxReg;
                row.MinReg = (byte)tour.MinReg;
                row.TourArea = tour.TourArea;
                row.TourCategory = tour.TourCategory;
                row.TourDescription = tour.TourDescription;
                row.TourDuration = (short)tour.TourDuration;
                row.TourLocation = tour.TourLocation;
                row.TourName = tour.TourName;
                row.TourPrice = tour.TourPrice;
                row.ImageData = tour.ImageData;
                row.ImageMimeType = tour.ImageMimeType;
                dc.SubmitChanges();
                return true;
            }
        }

        public bool EditUser(AUser user)
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {

                User row = (from c in dc.Users
                            where (c.UserID.ToString() == user.UserID)
                            select c).FirstOrDefault<User>();
                row.UserBirthday = user.UserBirthday;
                row.UserEmail = user.UserEmail;
                row.UserFirstName = user.UserFirstName;
                row.UserLastName = user.UserLastName;
                row.Username = user.Username;
                row.UserPassword = user.UserPassword;
                row.UserPhone = user.UserPhone;
                dc.SubmitChanges();
                return true;
            }
        }



          public bool DeleteEvent(string tourid, DateTime tourdate)
          {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {

                Event row = (from c in dc.Events
                             where (c.TourID.ToString() == tourid && c.TourDate == tourdate)
                             select c).FirstOrDefault<Event>();
                dc.Events.DeleteOnSubmit(row);

                // The deleted event's registrations must be deleted too. Otherwise, the sql servers throws an 
                // exeption.
                List<Registration> registrations= (from r in dc.Registrations
                              where (r.TourID.ToString() == tourid && r.TourDate == tourdate)
                              select r).ToList();
                foreach(Registration reg in registrations)
                {
                   dc.Registrations.DeleteOnSubmit(reg);
                }
                dc.SubmitChanges();
                return true;
            }

        
       }

          public bool DeleteReg(string id)
          {
              using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
              {

                  Registration row = (from c in dc.Registrations
                               where (c.RegID.ToString() == id)
                               select c).FirstOrDefault<Registration>();
                  dc.Registrations.DeleteOnSubmit(row);
                  dc.SubmitChanges();
                  return true;
              }
          }

          public bool DeleteTour(string id)
          {
              using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
              {

                  Tour row = (from c in dc.Tours
                                      where (c.TourID.ToString() == id)
                                      select c).FirstOrDefault<Tour>();
                  dc.Tours.DeleteOnSubmit(row);

                  // The deleted Tour's events must be deleted too. Otherwise, the sql servers throws an 
                  // exeption.
                  List<Event> events = (from e in dc.Events
                                                      where (e.TourID.ToString() == id)
                                                      select e).ToList();
                  foreach (Event e in events)
                  {
                      DeleteEvent(e.TourID.ToString(), e.TourDate); // this will also deleted all registrations for the event
                  }

                  dc.SubmitChanges();
                  return true;
              }
          }

          public bool DeleteUser(string id)
          {
              using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
              {

                  User row = (from c in dc.Users
                              where (c.UserID.ToString() == id)
                              select c).FirstOrDefault<User>();
                  dc.Users.DeleteOnSubmit(row);

                  // The deleted User's registrations must be deleted too. Otherwise, the sql servers throws an 
                  // exeption.
                  List<Registration> registrations = (from r in dc.Registrations
                                        where (r.UserID.ToString() == id)
                                        select r).ToList();
                  foreach (Registration reg in registrations)
                  {
                      dc.Registrations.DeleteOnSubmit(reg);
                  }

                  dc.SubmitChanges();
                  return true;
              }
          }
}
}


