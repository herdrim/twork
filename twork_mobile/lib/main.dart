// import 'package:flutter/material.dart';
// import 'package:twork_mobile/authentication.dart';
// import 'package:twork_mobile/routes.dart';

// void main() => runApp(new TWorkMobile());

// class TWorkMobile extends StatelessWidget {
//   // This widget is the root of your application.
//   @override
//   Widget build(BuildContext context) {
//     return MaterialApp(
//       title: 'TWork',
//       theme: ThemeData(
//         primarySwatch: Colors.blue,
//       ),
//       routes: routes,      
//     );
//   }
// }

import 'dart:async';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:io';

import 'package:twork_mobile/HomePage.dart';
 
class User {
  final String userId;
  final String userLogin;
  final String password;
 
  User({ this.userId, this.userLogin, this.password });
 
  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      userId: json['userId'],
      userLogin: json['userLogin'],
      password: json['password']
    );
  }
 
  Map toMap() {
    var map = new Map<String, dynamic>();
    // map["UserId"] = userId;
    map["UserLogin"] = userLogin;
    map["Password"] = password;
 
    return map;
  }
}
 
Future<User> createPost(String url, {Map body}) async {
  var bodyEncoded = json.encode(body);
  return http.post(url, body: bodyEncoded, headers: {"Accept": "application/json", "content-type": "application/json"}).then((http.Response response) {
    final int statusCode = response.statusCode;
 
    if (statusCode < 200 || statusCode > 400 || json == null) {
      throw new Exception("Error while fetching data. StatusCode: " + statusCode.toString());
    }
    return User.fromJson(json.decode(response.body));
  });
}
 
class TworkApp extends StatelessWidget{
  @override
  Widget build(BuildContext context) {
    return new MaterialApp(
        home: new LoginPage());
  }
}

class LoginPage extends StatelessWidget {
  final Future<User> post;
  final String userLogin;
  final String password; 

  LoginPage({Key key, this.userLogin, this.password, this.post}) : super(key: key);
  static final CREATE_POST_URL = 'http://192.168.0.11:85/api/account/login';
  TextEditingController titleControler = new TextEditingController();
  TextEditingController bodyControler = new TextEditingController();
 
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return Scaffold(
      appBar: AppBar(
        title: Text('Login'),
      ),
      body: new Container(
        margin: const EdgeInsets.only(left: 8.0, right: 8.0),
        child: new Column(
          children: <Widget>[
            new TextField(
              controller: titleControler,
              decoration: InputDecoration(
                  hintText: "UserLogin", labelText: 'Email'),
            ),
            new TextField(
              controller: bodyControler,
              decoration: InputDecoration(
                  labelText: 'Password'
                ),
                obscureText: true,
            ),
            new RaisedButton(
              onPressed: () async {
                User newPost = new User(
                    userId: "", userLogin: titleControler.text, password: bodyControler.text);
                User p = await createPost(CREATE_POST_URL,
                    body: newPost.toMap());
                if (p.userId != null && p.userId != ""){
                  Navigator.push(context, MaterialPageRoute(builder: (context) => HomePage(userLogin: p.userLogin, password: p.password)));
                }
              },
              child: const Text("Log in"),
            )
          ],
        ),
      )
    );
  }
}
 
void main() => runApp(TworkApp());