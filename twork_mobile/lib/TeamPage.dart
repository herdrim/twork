import 'package:flutter/material.dart';

import 'package:twork_mobile/MembersPage.dart';
import 'package:twork_mobile/TaskPage.dart';

class TeamPage extends StatelessWidget {  
  final String userLogin;
  final String password; 
  final String teamName;
  final int teamId;

  TeamPage({ this.userLogin, this.password, this.teamName, this.teamId });

  @override
  build(context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("MyTeam"),
      ),
      body: new Column(
        children: <Widget>[          
          Text(teamName, style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20.0),),
          Row(
            children: <Widget> [
              RaisedButton(
                child: Text("Members"),
                onPressed: () {
                  Navigator.push(context, 
                    MaterialPageRoute(builder: (context) =>
                      MembersPage(
                        userLogin: userLogin, 
                        password: password, 
                        teamId: teamId
                      )
                    )
                  );
                },
              ),
              RaisedButton(
                child: Text("Tasks"),
                onPressed: () {
                  Navigator.push(context, 
                    MaterialPageRoute(builder: (context) =>
                      TaskPage(
                        userLogin: userLogin, 
                        password: password, 
                        teamId: teamId
                      )
                    )
                  );
                },
              )
            ]
          )
        ],
      )
    );
  }
}