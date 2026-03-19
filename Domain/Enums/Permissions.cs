namespace Domain.Enums
{
    public enum Permissions
    {
        // =========================
        // Role Management (100 - 199)
        // =========================

        GetAllRoles = 100,
        CreateRole = 101,
        DeleteRole = 102,

        AssignPermissionToRole = 110,
        UnassignPermissionFromRole = 111,


        // =========================
        // User Authentication (200 - 299)
        // =========================

        RegisterUser = 200,
        LoginUser = 201,
        ViewOwnProfile = 202,


        // =========================
        // User Management (300 - 399)
        // =========================

        DeleteUser = 300,
        AssignRoleToUser = 301,
        UnassignRoleFromUser = 302,
        CreateStaffUser = 303,
        GetAllUsers = 304,


        // =========================
        // Facility Management (400 - 499)
        // =========================

        GetAllFacilities = 400,
        GetFacilityById = 401,
        CreateFacility = 402,
        UpdateFacility = 403,
        DeleteFacility = 404,


        // =========================
        // Room Management (500 - 599)
        // =========================

        CreateRoom = 500,
        DeleteRoom = 501,
        UpdateRoom = 502,
        GetAllRooms = 503,
        GetRoomById = 504,


        // =========================
        // Offer Management (600 - 699)
        // =========================
        GetAllOffers = 600,
        GetOfferById = 601,
        CreateOffer = 602,
        UpdateOffer = 603,
        DeleteOffer = 604,
        AddOfferToRoomType = 605,
        RemoveOfferFromRoomType = 606,


        // =========================
        // RoomType Management (700 - 799)
        // =========================

        GetAllRoomTypes = 700,
        GetRoomTypeById = 701,
        CreateRoomType = 702,
        UpdateRoomType = 703,
        DeleteRoomType = 704,

        AddFacilitiesToRoomType = 705,
        RemoveFacilitiesFromRoomType = 706,

        GetRoomTypesByFacility = 707,
        GetAvailableRoomTypes = 708,


        // =========================
        // Reservation Management (800 - 899)
        // =========================

        CreateReservation = 800,
        UpdateReservation = 801,
        DeleteReservation = 802,

        CancelReservation = 803,
        CheckInReservation = 804,
        CheckOutReservation = 805,

        GetReservationById = 806,
        GetUserReservations = 807

    }
}
