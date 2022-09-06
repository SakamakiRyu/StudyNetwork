<?php

$json = "";
if (isset($_POST['json'])) {
    $json = $_POST['json'];
}

$user = json_decode($json, true);

$user_id = $user["user_id"];
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

    // あるか判定
    $checkStmt = $pdo->prepare('select * from user where user_id = :user_id');
    $checkStmt->bindParam(':user_id', $user_id, PDO::PARAM_STR);
    $res = $checkStmt->execute();

    // あった場合
    if (empty($res)) {
        echo '既に登録されています';
        $pdo = null;
        return;
    }

    // SQL
    $stmt = $pdo->prepare('Insert into user (user_id,password,create_date) values (:user_id,:password,:date)');

    // バインド
    $stmt->bindParam(':user_id', $user_id, PDO::PARAM_STR);
    $stmt->bindParam(':password', $password, PDO::PARAM_STR);
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
