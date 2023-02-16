'''
https://docs.peewee-orm.com/en/latest/peewee/api.html
https://docs.peewee-orm.com/en/latest/peewee/quickstart.html
https://docs.python.org/3/library/csv.html
https://www.geeksforgeeks.org/python-convert-list-of-dictionaries-to-json/
'''

from peewee import *
import csv
import json

db = MySQLDatabase('ar', host='98.229.202.174', port=3307, user='root', password='yG6DH8W>bhR#}.0Q')

class robots(Model):
    name = CharField()
    ip_addr = CharField()
    port = IntegerField()
    image = CharField()
    class Meta:

        database = db
        db_table = 'robots'

class database_controller():
    def __init__(self):
        self.robots = robots()

    def add_to_table(self, nm, ip, prt, im):
        self.robots.create(
            name= nm,
            ip_addr = ip,
            port = prt,
            image = im,
        ).save()
    
    def update_table(self, index, nm, ip, prt, im):
        robot = self.robots.get(robots.id == index)
        robot.name= nm if nm != '' else robots.name
        robot.ip_addr = ip if ip != '' else robots.ip_addr
        robot.port = prt if prt != '' else robots.port
        robot.image = im if im != '' else robots.image
        robot.save()
    
    def delete(self, index):
        self.robots.delete().where(robots.id == index).execute()

    def getData(self):
        data = []
        for robot in robots.select():
            data.append({'id': robot.id, 'name': robot.name, 'ip_addr': robot.ip_addr, 'port': robot.port, 'image': robot.image})
        
        return json.dumps(data)

