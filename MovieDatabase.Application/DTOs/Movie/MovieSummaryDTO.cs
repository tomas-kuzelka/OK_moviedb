using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.DTOs.Movie;

public record MovieSummaryDTO
(
     string? Title,
     int ReleaseDate,
     bool IsAvailable

);
