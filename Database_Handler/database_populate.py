"""
https://docs.peewee-orm.com/en/latest/peewee/api.html
https://docs.peewee-orm.com/en/latest/peewee/quickstart.html
https://docs.python.org/3/library/csv.html
https://www.geeksforgeeks.org/python-convert-list-of-dictionaries-to-json/
"""

from peewee import *
import csv
import json

db = MySQLDatabase(
    "ar", host="98.229.202.174", port=3307, user="root", password="yG6DH8W>bhR#}.0Q"
)


class robots(Model):
    name = CharField()
    ip_addr = CharField()
    port = IntegerField()
    image = CharField()
    image_target_width = FloatField()

    class Meta:
        database = db
        db_table = "robots"


class augments(Model):
    robot_id = IntegerField()
    name = CharField()
    location = CharField()
    config = CharField()

    class Meta:
        database = db
        db_table = "augments"


class database_controller:
    def __init__(self):
        self.robots = robots()
        self.augments = augments()

    def add_robot(self, nm, ip, prt, im):
        self.robots.create(
            name=nm,
            ip_addr=ip,
            port=prt,
            image=im,
        ).save()

    def add_augment(self, robot_id, nm, loc, cfg):
        self.augments.create(
            robot_id=robot_id,
            name=nm,
            location=loc,
            config=cfg,
        ).save()

    def update_robot_table(self, index, nm, ip, prt, im):
        robot = self.robots.get(robots.id == index)
        robot.name = nm if nm != "" else robots.name
        robot.ip_addr = ip if ip != "" else robots.ip_addr
        robot.port = prt if prt != "" else robots.port
        robot.image = im if im != "" else robots.image
        robot.save()

    def update_augment_table(self, index, robot_id, nm, loc, cfg):
        augment = self.augments.get(augments.id == index)
        augment.robot_id = robot_id if robot_id != "" else augments.robot_id
        augment.name = nm if nm != "" else augments.name
        augment.location = loc if loc != "" else augments.location
        augment.config = cfg if cfg != "" else augments.config
        augment.save()

    def delete(self, index, table):
        if table == "robots":
            self.robots.delete().where(robots.id == index).execute()
        elif table == "augments":
            self.augments.delete().where(augments.id == index).execute()
        else:
            print("Invalid table name")

    def getData(self, table):
        data = []

        if table == "robots":
            for robot in robots.select():
                data.append(
                    {
                        "id": robot.id,
                        "name": robot.name,
                        "ip_addr": robot.ip_addr,
                        "port": robot.port,
                        "image": robot.image,
                        "image_target_width": robot.image_target_width,
                    }
                )
        elif table == "augments":
            for augment in augments.select():
                data.append(
                    {
                        "id": augment.id,
                        "robot_id": augment.robot_id,
                        "name": augment.name,
                        "location": augment.location,
                        "config": augment.config,
                    }
                )
        else:
            print("Invalid table name")
            return None

        return json.dumps(data)
