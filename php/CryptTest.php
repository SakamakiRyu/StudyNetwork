<?php
header("Content-Type:application/json;");
$src = "";

$AedIV = "1234567890123456";

if (isset($_POST['src'])) {
    $src = $_POST['src'];
}
 
$result = openssl_decrypt($src, 'AES-128-ECB', $AedIV);
echo $result;