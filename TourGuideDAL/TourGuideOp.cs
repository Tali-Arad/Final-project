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
                                        TourDescription = c.TourDescription
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
                                    where c.TourLocation == keyword || c.TourArea == keyword || c.TourCategory == keyword
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
                                         MaxReg = (int)c.MaxReg
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
                                     select new AEvent()
                                     {
                                         TourName = tours.TourName, 
                                         TourID = events.TourID.ToString(),
                                         TourDate = events.TourDate,
                                         TourOriginalDate = events.TourDate,
                                         TourGuide = events.TourGuide,
                                         IsOn = events.IsOn
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
                                     select new AEvent()
                                     {
                                         TourName = tours.TourName,
                                         TourID = events.TourID.ToString(),
                                         TourDate = events.TourDate,
                                         TourOriginalDate = events.TourDate,
                                         TourGuide = events.TourGuide,
                                         IsOn = events.IsOn
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
                                     select new AEvent()
                                     {
                                         TourName = tours.TourName,
                                         TourID = events.TourID.ToString(),
                                         TourDate = events.TourDate,
                                         TourOriginalDate = events.TourDate,
                                         TourGuide = events.TourGuide,
                                         IsOn = events.IsOn
                                     }
                              ).ToList<AEvent>();
                return rows;
            }
        }

        public List<AEvent> GetEventsByEventField(string sort) // gets events with sorting by guide or date
        {
            int month = DateTime.Parse("1." + sort + " 1900").Month;
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<AEvent> rows = (from events in dc.Events
                                     where events.TourGuide == sort || events.TourDate.Month == month
                                     join tours in dc.Tours on events.TourID equals tours.TourID
                                     select new AEvent()
                                     {
                                         TourName = tours.TourName,
                                         TourID = events.TourID.ToString(),
                                         TourDate = events.TourDate,
                                         TourOriginalDate = events.TourDate,
                                         TourGuide = events.TourGuide,
                                         IsOn = events.IsOn
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
                                     select new AEvent()
                                     {
                                         TourName = tours.TourName,
                                         TourID = events.TourID.ToString(),
                                         TourDate = events.TourDate,
                                         TourOriginalDate = events.TourDate,
                                         TourGuide = events.TourGuide,
                                         IsOn = events.IsOn
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
                                        IsOn = c.IsOn
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
                                        IsOn = events.IsOn
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
                                  TourDescription = c.TourDescription
                              }
                              ).FirstOrDefault<ATour>();
                return tour;
            }
        }

        public List<AReg> GetRegistrations() // gets all events
        {
            using (DataClassesTourGuideDataContext dc = new DataClassesTourGuideDataContext())
            {
                List<AReg> rows = (from c in dc.Registrations
                                     select new AReg()
                                     {
                                        RegID = c.RegID.ToString(),
                                        TourID = c.TourID.ToString(),
                                        TourDate = c.TourDate,
                                        UserID = c.UserID.ToString(),
                                        RegFirstName = c.RegFirstName,
                                        RegLastName = c.RegLastName,
                                        RegBirthday = c.RegBirthday,
                                        WillAttend = c.WillAttend,
                                        IsPaid = c.IsPaid,
                                        IsSentEmail = c.IsSentEmail,
                                        Attended = c.Attended,
                                        RegTime = c.RegTime   
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
                registration.WillAttend = (byte)reg.WillAttend;
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
                newEvent.IsOn = (byte)tourEvent.IsOn;
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
                    row.IsOn = (byte)tourEvent.IsOn;
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
                        newReg.IsSentEmail = reg.IsSentEmail;
                        newReg.IsPaid = reg.IsPaid;
                        newReg.Attended = reg.Attended;
                        newReg.RegFirstName = reg.RegFirstName;
                        newReg.RegLastName = reg.RegLastName;
                        newReg.RegBirthday = reg.RegBirthday;
                        newReg.TourDate = tourEvent.TourDate;
                        newReg.TourID = tourEvent.TourID;
                        newReg.UserID = reg.UserID.ToString();
                        newReg.WillAttend = (byte)reg.WillAttend;
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
                row.IsSentEmail = (byte)reg.IsSentEmail;
                row.IsPaid = (byte)reg.IsPaid;
                row.Attended = (byte)reg.Attended; ;
                row.RegFirstName = reg.RegFirstName;
                row.RegLastName = reg.RegLastName;
                row.RegBirthday = reg.RegBirthday;
                row.WillAttend = (byte)reg.WillAttend;
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


