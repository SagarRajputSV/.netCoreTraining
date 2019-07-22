namespace TrainingManagement.API
{
    public class Constants
    {
        public static readonly string AlreadyRegisterd = "You already registered for this training";
        public static readonly string LinkAlreadyUsed = "Clicked link already used.";
        public static readonly string AlreadySubmitted = "You already submitted your answers for this training";
        public static readonly string ThankYouForYourAnswer = "We have recived your answer. Your seat confirmed now.";
        public static readonly string SuccessRegisterdMessage = "Thank you for registering, we have sent one link to registerd email-id, please go and complete pre assessment task to confirm your seat.";
        public static readonly string FaileRegisterdMessage = "Unable to enroll. Please try again";
        public static readonly string RequestProcessingFailed = "We are unable to process your request now, please try again.";
    }

    public class EmailContent
    {
        public static readonly string ForgotPassword = "We received a request to change your password on <b>AutoCare TrendLens</b>. Click the below button to set a new password. ";
        public static readonly string AddUser = "Welcome <b>Softvision Cognizent </b> training portal, your account has been created successfully. Please click below to confirm your email account.";
        public static readonly string PreEnrollmentExam = "Welcome <b>Softvision Cognizent </b> Training Portal, please click below link and complete the test to confirm your seat before {0}. Link will not work after {1}.</br>Please don't refresh page once you start.";
        public static readonly string UserVerification = "Welcome <b>Softvision Cognizent </b> training portal, your account has been created successfully. Please click below to confirm your email account.";
        public static readonly string PasswordReset = "You have successfully reset your password.";
        public static readonly string LinkValidity = "Note the link is valid for 24 hours.";
    }
}
