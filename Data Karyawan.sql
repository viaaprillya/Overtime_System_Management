-- 1. Menambahkan data Jabatan
INSERT INTO Jabatan (NamaJabatan, GajiPokok, Tunjangan)
VALUES ('Direktur', '25000000', '5000000'),
('Manajer', '15000000', '3000000'),
('Karyawan Teknis', '7500000', '1500000'),
('Karyawan Non Teknis', '5000000', '1500000');

SELECT * FROM Jabatan

-- 2. Menambahkan data Karyawan
INSERT INTO Karyawan (NamaLengkap, NomerTelepon, NomerRekening, Email,JabatanID)
VALUES ('Agus Kuncoro', '081234567891', '897654321','agus@email.com', '1'),
('Siti Kusmini', '081234567899', '897654123','siti@email.com', '2'),
('Asa Kabaret', '081234567898', '897654124','asa@email.com', '3'),
('Muhammad Abidin', '081234567897', '897654125','abidin@email.com', '3'),
('Gita Devi', '081234567896', '897654126','gita@email.com', '4'),
('Marie Safitri', '081234567895', '897654127','marie@email.com', '4');

SELECT * FROM Karyawan -- check isi tabel Karyawan

-- 3. Menambahkan data Lembur
INSERT INTO Lembur (KaryawanID, Tanggal, Durasi, Keterangan, Approval)
VALUES ('3', '2022-09-10', 2, 'Bertemu Client','Processing'),
('4', '2022-09-10', 3, 'Sorting Dokumen','Processing'),
('4', '2022-09-12', 1, 'Bertemu Client','Processing'),
('5', '2022-10-10', 2,'Input Data','Processing');

SELECT * FROM Lembur -- check isi tabel Lembur