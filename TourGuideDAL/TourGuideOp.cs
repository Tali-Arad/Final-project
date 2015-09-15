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
                                        TourPrice = (double)c.TourPrice,
                                        MinReg = (int)c.MinReg,
                                        MaxReg = (int)c.MaxReg
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
                                         TourPrice = (double)c.TourPrice,
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
                                  TourPrice = (double)c.TourPrice,
                                  MinReg = (int)c.MinReg,
                                  MaxReg = (int)c.MaxReg,
                                  TourDescription = c.TourDescription
                              }
                              ).FirstOrDefault<ATour>();
                return tour;
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
                registration.RegID = System.Guid.NewGuid();
                registration.RegTime = DateTime.Now;
                registration.WillAttend = (byte)reg.WillAttend;
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



        }
       }


