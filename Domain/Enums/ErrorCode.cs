
namespace Domain.Enums
{
    public enum ErrorCode
    {
        NoError = 0,
        ServerError = 1,


        ValidationError = 101,
        InvalidCredentials = 102,

        UserNameRequired = 110,
        EmailRequired = 111,
        InvalidEmailFormat = 112,
        EmailAlreadyExists = 113,
        EmailTooLong = 114,
        UserAlreadyExists = 115,

        PasswordRequired = 120,
        PasswordTooLong = 121,
        PasswordTooShort = 122,
        PasswordMismatch = 123,
        ConfirmPasswordRequired = 124,

        PhoneNumberRequired = 130,
        CountryRequired = 131,

        RoleNotFound = 140,
        RoleAlreadyExists = 141,
        UserNotFound = 142,
        DuplicateEntry = 143,
        RolePermissionNotFound = 144,
        UserRoleNotFound = 145,
        RoleRequired = 146,
        UserRequired = 147,
        PermissionRequired = 148,
        PermissionNotFound = 149,


        // Offer related errors
        OfferCodeAlreadyExists = 301,
        OfferDescriptionRequired = 302,
        OfferStartDateRequired = 303,
        OfferEndDateRequired = 304,
        OfferInvalidEndDate = 305,
        OfferInvalidDiscountValue = 306,
        OfferNotFound = 307,
        OfferCodeRequired = 308,
        OfferInvalidStartDate = 309,
        OfferDiscountValueRequired = 310,
        OfferIdRequired = 311,
        OfferNotAssigned = 312,

        // Room related errors
        RoomNotFound = 600,              
        RoomNumberAlreadyExists = 601,   
        RoomPriceInvalid = 602,          
        RoomCapacityInvalid = 603,
        RoomNumberRequired = 604,
        RoomStatusRequired = 605,
        RoomIdRequired = 606,
        RoomsSoldOut = 607,


        // RoomType related errors
        RoomTypeRequired = 701,
        RoomTypeNotFound = 702,
        RoomTypeNameRequired = 703,
        RoomTypeDescriptionRequired = 704,
        RoomTypeBasePriceRequired = 705,
        RoomTypeMaxCapacityRequired = 706,
        RoomTypeCodeRequired = 707,
        RoomTypeIdRequired = 708,
        RoomTypeAlreadyExists = 709,
        CapacityExceeded = 710,



        // Facility related errors
        FacilityRequired = 801,
        FacilityNotFound = 802,
        FacilityAlreadyExists = 803,
        FacilityNotExists = 804,
        FacilityNameRequired = 805,
        FacilityNameTooLong = 806,
        FacilityDescriptionRequired = 807,
        FacilityDescriptionTooLong = 808,
        FacilityCodeRequired = 809,
        FacilityCodeTooLong = 810,
        FacilityIdRequired = 811,

        // Reservation related errors
        ReservationCheckInDateRequired = 901,
        ReservationCheckOutDateRequired = 902,
        ReservationRoomTypeBookingRequired = 903,
        ReservationCheckInDateInvalid = 904,
        ReservationCheckOutDateInvalid = 905,
        ReservationIdRequired = 906,
        ReservationNotFound = 907,


    }
}
