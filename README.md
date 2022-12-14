# Overtime_System_Management
Evaluasi Akhir DTSXMCC

## Hal-hal yang harus diperhatikan sebelum menjalankan program:
### 1. Ganti ConnectionStrings pada [appsetings.json](https://github.com/viaaprillya/Overtime_System_Management/blob/MVC/API/appsettings.json)
Ganti sesuai dengan koneksi string ke database yang kalian punya  
  
### 2. Tambahkan folder sbadmin ke dalam [wwwroot](https://github.com/viaaprillya/Overtime_System_Management/tree/MVC/Overtime_System_Management/wwwroot)
Silahkan download template [SB Admin 2](https://startbootstrap.com/theme/sb-admin-2) dan copy folder css/img/js/scc/vendor ke dalam wwwroot

![image](https://user-images.githubusercontent.com/37134829/194742168-eeb3bc00-f985-493f-a8cd-b32422663b5e.png)

### 3. Pastikan isi pada database table Role
  - Role dengan ID 1 adalah Admin
  - Role dengan ID 2 adalah User
  
   
   
#### NB 
1. Skema database  
![image](https://user-images.githubusercontent.com/37134829/194742464-80d31b47-9127-4f3b-88e3-e084613ddd90.png)  

![image](https://user-images.githubusercontent.com/37134829/194742669-002da5d6-0336-40de-8557-856dc18dbc2e.png)


2. Jika belum membuat user sama sekali
  - Sebaiknya gunakan API registrasi agar user secara default dapat login menggunakan password yang nilainya sama dengan nomor telepon
  - Kemudian jika ingin menjadikan role menjadi admin dapat diubah secara manual melalui table UserRole
  
#### Saran setelah presentasi
  - Tambahkan timesheet saat melakukan request lembur (ex: 19.00-20.00 = "....", 20.00-21.00 = "....")
  - Tentukan batasan jam mulai lembur
  - Pending button jangan disatukan dengan proses approve lembur
  - Batasan lembur per hari, per minggu dan per bulan
  - Perhitungan upah lembur sebaiknya jangan di hardcode, buat table dalam database apabila ada kemungkinan perhitungan berubah
  - Chart diberi label agar mudah dibaca
  - Warna button diubah (pakai icon?)
  - Filter tanggal pengajuan lembur
  
  
  
