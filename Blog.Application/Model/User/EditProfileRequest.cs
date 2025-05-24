using Blog.Domain.Enum;

namespace Blog.Application.Model.User;

public record EditProfileRequest(string FullName, Gender Gender, string BirthPlace, string Bio);

