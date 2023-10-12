using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnDestroyData : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public struct data_token
    {
        public string name;
        public string token;
    }
    //fantomfathomfirm@gmail.com_token
#if UNITY_EDITOR
    //public static string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoiYzBhN2Y5NWYtZjBiOS00MWQ3LTkxYzAtZDVlMGI1MzYzNmIwIiwidXNlcm5hbWUiOiJmYW50b21mYXRob21maXJtQGdtYWlsLmNvbSIsImV4cCI6MTY1Mjg5Nzk1OCwiZW1haWwiOiJmYW50b21mYXRob21maXJtQGdtYWlsLmNvbSIsIm9yaWdfaWF0IjoxNjQ5NDQxOTU4LCJpc3MiOiJBZHZlbnR1cmVUcmF2ZWxlciJ9.h5rl4u1tUfQ-6CQh6YeuU5-U9aW6Jkgw-pvVuhqrWo4";
    //public static string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoiMThhNTM3NTMtZWYyYi00ZWUyLWE0ZTEtZGUwYTU0N2MzMjU2IiwidXNlcm5hbWUiOiJ0bWRkb2dAaG90bWFpbC5jb20iLCJleHAiOjE2NTI4OTg2OTcsImVtYWlsIjoidG1kZG9nQGhvdG1haWwuY29tIiwib3JpZ19pYXQiOjE2NDk0NDI2OTcsImlzcyI6IkFkdmVudHVyZVRyYXZlbGVyIn0.5eXovfbVihRzaUqA5PfmuO0g62JDy1k4y963x5UlSKk";
    private static bool mFirstShow = true;
    private static string mToken;
    public static string token
    {
        get
        {
            if (string.IsNullOrEmpty(mToken) && ES3.KeyExists("AutoLogin"))
            {
                mToken = ES3.Load<string>("AutoLogin");
            }
            else
            {
                if (mFirstShow)
                {
                    mFirstShow = false;
                    Debug.Log("Not Get Cache -> " + mToken);
                }
                //mToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoiODUyZjg5ZDctNGQyYi00OWMwLWFiMzEtMDU5YjhjNGVjM2EyIiwidXNlcm5hbWUiOiJjdXRlc2FtbGxwZ2luQGdtYWlsLmNvbSIsImV4cCI6MTY2NDI3NDk3MywiZW1haWwiOiJjdXRlc2FtbGxwZ2luQGdtYWlsLmNvbSIsIm9yaWdfaWF0IjoxNjYwODE4OTczLCJpc3MiOiJBZHZlbnR1cmVUcmF2ZWxlciJ9.EmK0yDBErG5Kp7kFZHUee40zLrxU7dpGhxwyfxAfRdI";
            }
            return mToken;
        }
        set => mToken = value;
    }
    //public static string token = "";
#else
    public static string token = "";
#endif
    public struct ProgressData
    {
        public string progress_id;

        public bool is_done_eager_beaver;
        public int done_eager_beaver;
        public int total_eager_beaver;

        public bool is_done_journey_director;
        public int done_journey_director;
        public int total_journey_director;

        public bool is_done_luminary_icon;
        public int done_luminary_icon;
        public int total_luminary_icon;

        public bool is_done_aviator_timer;
        public int done_aviator_timer;
        public int total_aviator_timer;

        public bool is_done_turbo_simmer;
        public int done_turbo_simmer;
        public int total_turbo_simmer;

        public bool is_done_brewed_passion;
        public int done_brewed_passion;
        public int total_brewed_passion;

        public bool is_done_wisdom_tree;
        public int done_wisdom_tree;
        public int total_wisdom_tree;

        public bool is_done_marathon;
        public int done_marathon;
        public int total_marathon;

        public bool is_done_edge_cutter;
        public int done_edge_cutter;
        public int total_edge_cutter;

        public string owner;
    }

    public static ProgressData progressData;

    public struct AwardData
    {
        public string award_id;
        public bool is_unblock_eager_beaver;
        public bool is_unblock_the_explorer;
        public bool is_unblock_horizon_summit;
        public bool is_unblock_go_getter;
        public bool is_unblock_passage_of_sliver;
        public bool is_unblock_extra_mile;
        public bool is_unblock_wander_luster;
        public bool is_unblock_vibrant_notch;
        public bool is_unblock_travel_bug;
        public bool is_unblock_thurlodge_pass;
        public bool is_unblock_herculean_ring;
        public bool is_unblock_adventure_traveller;
        public string owner;
    }

    public static AwardData awardData;

    public struct AccountData
    {
        public string email;
        public string type;
        public string password;
    }

    public static AccountData accountData;

    public struct JourneyData
    {
        public string duration;
        public double distance;
        public string name;
        public string category;
    }

    public static JourneyData journeyData;

    /// <summary>
    /// For update a exist account data.
    /// </summary>
    public struct UserData
    {
        public string email;
        public string lang;
        public string created_at;
        public string account_type;
        public string last_name;
        public string first_name;
        public string birthday_day;
        public string gender;
        public string height;
        public string weight;
        public string blood_type;
        public string medical_allergies;
        public string emergency_contact_number;
        public string list_of_medical_conditions;
        public string additional_notes;
        public string status;
        public string full_name;
        public string user_name;
    }

    public static UserData userData;

    public static bool IsGusetLogin = false;
    /// <summary>
    /// For register account.
    /// </summary>
    public struct UploadUserData
    {
        public string last_name;
        public string first_name;
        public string birthday_day;
        public string gender;
        public string height;
        public string weight;
        public string blood_type;
        public string medical_allergies;
        public string emergency_contact_number;
        public string list_of_medical_conditions;
        public string additional_notes;
        public string user_name;
    }

    public static UploadUserData _UpLoadUserData;

    public struct BoaterData
    {
        public string boatType;
        public string boatStyle;
        public string boatRegistration;
        public string boatHullColours;
        public string boatTopColours;
        public string boatTrailer;
        public string trailerLicense;
        public string towVehicle;
        public string vehicleYear;
        public string vehicleColour;
        public string vehicleLicenseNo;
        public string additionalNotes;
    }
    public static BoaterData _BoaterData;


    public struct CrewData
    {
        public string emergencyContact1;
        public string crewNumber1;
        public string relationshipType1;

        public string emergencyContact2;
        public string crewNumber2;
        public string relationshipType2;

        public string emergencyContact3;
        public string crewNumber3;
        public string relationshipType3;
    }
    public CrewData _CrewData;

    [Serializable]
    public struct Journeys
    {
        public List<JourneyMsg> msg;
        //public Dictionary<string, JourneyMsg> msg { get; set; }
        public int current_page;
        public int total_page;
    }

    public static Journeys journeys;

    [Serializable]
    public struct JourneyMsg
    {
        public string journey_id;
        public string name;
        public string duration;
        public string distance;
        public string created;
        public List<MsgResources> resources;
        public string category;
        //public Dictionary<string, MsgResources> resources { get; set; }
    }

    [Serializable]
    public struct MsgResources
    {
        public string resource_url;
        public string resource_id;
    }

    public struct NewJourneyData
    {
        public string journey_id;
        public string name;
        public string duration;
        public string distance;
        public string created;
        public string resources;
        public string category;
    }

    public static NewJourneyData newJourneyData;

    public struct ErrorMsg
    {
        public string msg;
    }
}