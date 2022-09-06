<?php

$json = "";
if (isset($_POST['json'])) {
    $json = $_POST['json'];
}

// Jsonデコード
$user = json_decode($json, true);
var_dump($user);

$user_id = $user["userID"];
$password = $user["password"];

// 不正な入力の場合ははじく
if ($user_id == "" || $password == "") {
    return;
}

// 現在の日付と時刻
$create_at = new DateTime();

try {
    // DBに接続
    $pdo = new PDO('mysql:charset=utf8;dbname=dragon;host=localhost;', 'root', 'r35ryu0311');

    // SQL
    $stmt = $pdo->prepare('Insert into user (user_id,password,create_date) values (:user_id,:password,:date)');

    // バインド
    $stmt->bindParam(':user_id', $user_id, PDO::PARAM_STR);
    $stmt->bindParam(':password', hash('sha256',$password) , PDO::PARAM_STR);
    $stmt->bindParam(':date', $create_at->format('Y-m-d H:i:s'), PDO::PARAM_STR);

    // 命令実行
    $res = $stmt->execute();

    if ($res) {
        echo "ユーザー作成成功！";
    }
} catch (PDOException $e) {
    echo $e->getMessage();
} finally {
    // 切断
    $pdo = null;
}
