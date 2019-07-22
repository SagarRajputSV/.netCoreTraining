using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingManagement.API.UtilitiesHelper;
using TrainingManagement.Application.ApplicationServices;
using TrainingManagement.Application.BusinessServices;
using TrainingManagement.Application.RepositoryService;

namespace TrainingManagement.API
{
    public static class ServiceExtensions
    {
        //public static IServiceCollection RegisterServices(
        //    this IServiceCollection services, string connectionString)
        //{
        //    return services;
        //}
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ITrainingManagementUnitOfWork, TrainingManagementUnitOfWork>();
            services.AddSingleton<Func<ITrainingManagementUnitOfWork>>(container => () =>
            {
                var scope = container.GetService<IServiceScopeFactory>().CreateScope();
                var instance = scope.ServiceProvider.GetRequiredService<ITrainingManagementUnitOfWork>();
                instance.AssignScope(scope);
                return instance;
            });
            services.AddTransient(typeof(ITrainingManagementBusinessServices<>), typeof(TrainingManagementBusinessService<>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddTransient<IApplicationServices, ApplicationServices>();
            services.AddTransient<ITopHeaderInformationApplicationServices, TopHeaderInformationApplicationServices>();
            services.AddTransient<ITopHeaderInformationBusinessServices, TopHeaderInformationBusinessServices>();
            services.AddTransient<IFeedbackQuestionApplicationServices, FeedbackQuestionApplicationServices>();
            services.AddTransient<IFeedbackQuestionBusinessServices, FeedbackQuestionBusinessServices>();
            services.AddTransient<ITrainerApplicationServices, TrainerApplicationServices>();
            services.AddTransient<ITrainerBusinessServices, TrainerBusinessServices>();
            services.AddTransient<ISubjectApplicationService, SubjectApplicationService>();
            services.AddTransient<ISubjectBusinessService, SubjectBusinessService>();
            services.AddTransient<IUtilitiesHelpers, UtilitiesHelpers>();
            services.AddTransient<ITraineeFeedbackApplicationServices, TraineeFeedbackApplicationServices>();
            services.AddTransient<ITraineeFeedbackBusinessServices, TraineeFeedbackBusinessServices>();
            services.AddTransient<ITaineerSubjectApplicationServices, TaineerSubjectApplicationServices>();
            services.AddTransient<ITaineerSubjectBusinessServices, TaineerSubjectBusinessServices>();
            services.AddTransient<IImagesApplicationServices, ImagesApplicationServices>();
            services.AddTransient<IImagesBusinessService, ImagesBusinessService>();
            services.AddTransient<IPreEnrollmentQuestionApplicationServices, PreEnrollmentQuestionApplicationServices>();
            services.AddTransient<IPreEnrollmentQuestionBusinessServices, PreEnrollmentQuestionBusinessServices>();
            services.AddTransient<ITrainingSubjectApplicationService, CourseEnrollmentApplicationServices>();
            services.AddTransient<ICourseEnrollmentBusinessServices, CourseEnrollmentBusinessServices>();
            services.AddTransient<IPreEnrollmentUserAnswerApplicationServices, PreEnrollmentUserAnswerApplicationServices>();
            services.AddTransient<IPreEnrollmentUserAnswerBusinessServices, PreEnrollmentUserAnswerBusinessServices>();

            return services;
        }
    }
}
