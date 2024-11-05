using Kurzy.Controllers;
using Kurzy.Models;
using Kurzy.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kurzy.Models.DtoModels;

namespace Kurzy.Tests
{
    public class CourseControllerTests
    {
        private readonly Mock<ICourseService> _mockCourseService;
        private readonly Mock<ILogger<CourseController>> _mockLogger;
        private readonly CourseController _controller;

        public CourseControllerTests()
        {
            _mockCourseService = new Mock<ICourseService>();
            _mockLogger = new Mock<ILogger<CourseController>>();
            _controller = new CourseController(_mockLogger.Object, _mockCourseService.Object);
        }

        [Fact]
        public void GetCourses_ReturnsOkResult_WithListOfCourses()
        {
            // Arrange
            var services = new Services();
            var mockCoursesDto = services.MockCoursesDto();
            _mockCourseService.Setup(service => service.GetCourses()).Returns(mockCoursesDto);

            // Act
            var result = _controller.GetCourses() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var dtos = result.Value as List<CourseDto>;
            Assert.NotNull(dtos);
            Assert.Equal(mockCoursesDto.Count, dtos.Count);

            for (int i = 0; i < dtos.Count; i++)
            {
                Assert.Equal(mockCoursesDto[i].Id, dtos[i].Id);
                Assert.Equal(mockCoursesDto[i].Name, dtos[i].Name);
                Assert.Equal(mockCoursesDto[i].Credits, dtos[i].Credits);
                Assert.Equal(mockCoursesDto[i].Description, dtos[i].Description);
                Assert.Equal(mockCoursesDto[i].Students.Count, dtos[i].Students.Count);

                for (int j = 0; j < dtos[i].Students.Count; j++)
                {
                    Assert.Equal(mockCoursesDto[i].Students[j].Id, dtos[i].Students[j].Id);
                    Assert.Equal(mockCoursesDto[i].Students[j].FirstName, dtos[i].Students[j].FirstName);
                    Assert.Equal(mockCoursesDto[i].Students[j].LastName, dtos[i].Students[j].LastName);
                }
            }
        }
    }
}
