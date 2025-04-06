using Blog.Domain.Enum;

namespace Blog.Application.Model.Profile;

public record EditProfileRequest(string FullName, Gender Gender, string BirthPlace, string Bio, string ProfileImageLink);

