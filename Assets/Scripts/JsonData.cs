public partial class SudukuJSON
{
    public class JsonData
    {
        public string grid_mode;
        public string times_;
        public string wrongs_;
        public string solved_data;
        public string unsolved_data;
        public string unsolved_data_;

        public JsonData(string grid_mode, string times_, string wrongs_, string solved_data, string unsolved_data, string unsolved_data_)
        {
            this.grid_mode = grid_mode;
            this.times_ = times_;
            this.wrongs_ = wrongs_;
            this.solved_data = solved_data;
            this.unsolved_data = unsolved_data;
            this.unsolved_data_ = unsolved_data_;
        }
    }
}

