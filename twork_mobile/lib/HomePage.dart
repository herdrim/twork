import 'dart:async';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:io';

import 'package:twork_mobile/TeamPage.dart';
 
class Team {
  int teamId;
  String teamName;
 
  Team(int teamId, String teamName){
    this.teamId = teamId;
    this.teamName = teamName;
  }
 
  Team.fromJson(Map json) : teamId = json['teamId'], teamName = json['teamName'];
 
  Map toJson() {
    return { 'teamId': teamId, 'teamName': teamName };
  } 
}

class MyTeam{
  static Future<http.Response> getTeams(String userLogin, String password) async {
    var url =  'http://192.168.0.11:85/api/MyTeam/GetTeams';    
    return await http.post(url, body: json.encode({ 'UserLogin': userLogin, 'Password': password }),
      headers: { "Accept": "application/json", "content-type": "application/json" });    
  }
}
 
class HomePage extends StatefulWidget {
  final String userLogin;
  final String password;
  
  HomePage({Key key, this.userLogin, this.password}) : super(key: key);
  @override
  createState() => HomePageState(userLogin: userLogin, password: password);
}

class HomePageState extends State {
  final String userLogin;
  final String password;

  HomePageState ({ this.userLogin, this.password });

  var teams = new List<Team>();

  _getTeams() {
    MyTeam.getTeams(userLogin, password).then((http.Response response) {
      setState(() {
       Iterable list = json.decode(response.body);
       teams = list.map((model) => Team.fromJson(model)).toList();
      });
    });
  }

  initState() {
    super.initState();
    _getTeams();
  }

  dispose() {
    super.dispose();
  }

    @override
  build(context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("MyTeams"),
      ),
      body: ListView.builder(
        itemCount: teams.length,
        itemBuilder: (context, index) {
          return ListTile(
            title: Text(teams[index].teamName),
            onTap: () => 
              Navigator.push(context, 
                MaterialPageRoute(builder: (context) =>
                   TeamPage(
                     userLogin: userLogin, 
                     password: password, 
                     teamId: teams[index].teamId, 
                     teamName: teams[index].teamName
                  )
                )
              ),
          );
        },
      )
    );
  }
}