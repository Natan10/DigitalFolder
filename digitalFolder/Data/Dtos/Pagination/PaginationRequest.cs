namespace DigitalFolder.Data.Dtos.Pagination
{
    public class PaginationRequest
    {

        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
    }
}
