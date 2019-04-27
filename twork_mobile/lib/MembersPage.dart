import 'dart:async';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
 
class Member {
  String userId;
  String userName;
  String email;
  List<Role> roles;
 
  Member({ this.userId, this.userName, this.email, this.roles });
 
  factory Member.fromJson(Map<String, dynamic> json) { 
    var list = json['roles'] as List;
    List<Role> roles = list.map((i) => Role.fromJson(i)).toList();
    return Member (
      userId: json['userId'],
      userName: json['userName'],
      email: json['email'],
      roles: roles
    );
  }
}

class Role {
  int roleId;
  String roleName;
  String roleDescription;

  Role ({ this.roleId, this.roleDescription, this.roleName });
 
  factory Role.fromJson(Map<String, dynamic> json) { 
    return Role(
      roleId: json['roleId'],
      roleName: json['roleName'],
      roleDescription: json['roleDescription']
    );
  }
}

class TeamMember{
  static Future<http.Response> getMembers(String userLogin, String password, int teamId) async {
    var url =  'http://192.168.0.11:85/api/Member/GetMembers';    
    return await http.post(url, body: json.encode({ 'UserLogin': userLogin, 'Password': password, 'TeamId': teamId }),
      headers: { "Accept": "application/json", "content-type": "application/json" });    
  }
}
 
class MembersPage extends StatefulWidget {
  final String userLogin;
  final String password;
  final int teamId;
  
  MembersPage({Key key, this.userLogin, this.password, this.teamId}) : super(key: key);
  @override
  createState() => MembersPageState(userLogin: userLogin, password: password, teamId: teamId);
}

class MembersPageState extends State {
  final String userLogin;
  final String password;
  final int teamId;

  MembersPageState ({ this.userLogin, this.password, this.teamId });

  var members = new List<Member>();

  String _buildRolesString (int index){
    String roles = "Roles: ";
    int i = 0;
    members[index].roles.forEach((role) {
      if (i == 0) 
        roles += role.roleName;
      else
         roles += ", " + role.roleName;
      i++;
    });
    return roles;
  }

  _getMembers() {
    TeamMember.getMembers(userLogin, password, teamId).then((http.Response response) {
      setState(() {
       Iterable list = json.decode(response.body);
       members = list.map((model) => Member.fromJson(model)).toList();
      });
    });
  }

  initState() {
    super.initState();
    _getMembers();
  }

  dispose() {
    super.dispose();
  }

    @override
  build(context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("TeamMembers"),
      ),
      body: ListView.separated(
        separatorBuilder: (context, index) => Divider(
          color: Colors.black,
        ),
        itemCount: members.length,
        itemBuilder: (context, index) {
          return Padding(
            padding: EdgeInsets.all(10.0),
            child: Column(
              children: <Widget>[
                Text(
                  members[index].userName,
                  style: Theme.of(context).textTheme.body2,
                ),
                Container(alignment: Alignment.centerLeft, child: Text (_buildRolesString(index)),) 
              ],
            ),
          );         
        },
      ),
    );
  }
}