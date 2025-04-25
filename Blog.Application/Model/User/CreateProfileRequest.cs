using Blog.Domain.Enum;

namespace Blog.Application.Model.User;

public record CreateProfileRequest(string FullName, Gender Gender, string BirthPlace, string Bio, string ProfileImageLink);
