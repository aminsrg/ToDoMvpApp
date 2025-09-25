namespace ToDoMvpApp.Common.SharedModel;
public record ApiResponse<T>(T? Data, bool Success = true, string? Error = null);
