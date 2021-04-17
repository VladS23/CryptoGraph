# CryptoGraph
## RUS
Это небольшая программа для шифрования вашего общения использует алгоритм RSA-2048 для передачи ключей и алгоритм AES для шифрования данных
### Инструкция
Перед началом использования рекомендуется поместить программу CriptoGraph.exe в отдельную папку (текущая папка - CryptoGraph/WindowsFormsApp7/bin/Debug/)  
Первый пользователь должен:
1. Создать ключ RSA
2. Отправить файлы "RSAKey1" и "RSAKey2" другому пользователю любым удобным методом  
Второй пользователь должен:  
3. Поместить RSAKey1" и "RSAKey2" в папку с программой и импортировать ключи RSA
4. Создать и отправить другому пользователю ключи AES  
Первый пользователь должен:  
5. Поместить AESKey1" и "AESKey2" в папку с программой и импортировать ключи AES  
  
#### Приятного общения    
  
При нажатии кнопки "Расшифровать" файл будет расшифрован

## EN
This is a small program to encrypt your conversations  
### Instructions:  
User 1 must  
1. Place the CriptoGraph.exe in a separate folder (the current folder is CryptoGraph/WindowsFormsApp7/bin/Debug/)  
2. Click on "Создать ключ RSA" button  
3. Send files "RSAKey1" and "RSAKey2" to another person  
User 2 must  
4. Place the CriptoGraph.exe in a separate folder (the current folder is CryptoGraph/WindowsFormsApp7/bin/Debug/)  
5. Plase "RSAKey1" and "RSAKey2"to the program folder and click on "Импортировать ключ RSA" button  
6. Click on "Создать ключ AES" button  
7. Send files "AESKey1" and "AESKey2" to another person  
User 1 must   
8. Plase "AESKey1" and "AESKey2 "to the program folder and click on "Импортировать ключ AES" button  

Click on the "Зашифровать" button to encrypt  
Click on the "Расшифровать" button to decrypt
The file with the decrypted data will be created in the current directory  
