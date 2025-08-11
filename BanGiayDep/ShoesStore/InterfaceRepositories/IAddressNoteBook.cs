using ShoesStore.Models;

namespace ShoesStore.InterfaceRepositories
{
    public interface IAddressNoteBook
    {
        public List<Tinh> GetTinhList();
        public List<Quan> GetQuanList(int provinces);
        public List<Phuong> GetPhuongList(int districts);
        public List<Sodiachi> GetAllAddressNote();

        public List<Sodiachi> GetAllAddressNoteByMaKH(int maKH);

        public List<Sodiachi> GetAllAddressNoteById(int masdc);

        public void AddAddressNote(int proviceId, int districtId, int wardId, string address,int makh, string tennguoinhan, string sdt);
        public Sodiachi GetSodiachi(int masodiachi);
        public int GetMaTinh(string TenTinh);
        public int GetMaQuan(string TenQuan);
        public int GetMaPhuong(string TenPhuong);
        public void UpdateSDC(int masdc, string hoten, string sdt, string diachi, int matinh, int maquan, int maphuong);
        public void DeleteSDC(int masdc);
    }
}
