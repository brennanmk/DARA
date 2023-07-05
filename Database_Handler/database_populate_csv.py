"""
https://docs.peewee-orm.com/en/latest/peewee/api.html
https://docs.peewee-orm.com/en/latest/peewee/quickstart.html
https://docs.python.org/3/library/csv.html
"""

from peewee import *
import csv

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

    def create_tables(self):
        self.robots.create_table()
        self.augments.create_table()

    def drop_tables(self):
        self.robots.drop_table()
        self.augments.drop_table()

    def populate_tables(self):
        with open("robots.csv") as csv_file:
            data = csv.DictReader(csv_file)

            for vals in data:
                self.robots.create(
                    name=vals["robot_name"],
                    ip_addr=vals["ros_master_uri"],
                    port=vals["ros_master_port"],
                    image=vals["image_target_location"],
                    image_target_width=float(vals["image_target_width"]),
                ).save()

        with open("augments.csv") as csv_file:
            data = csv.DictReader(csv_file)

            for vals in data:
                self.augments.create(
                    robot_id=vals["id"],
                    name=vals["name"],
                    location=vals["location"],
                    config=vals["config"],
                ).save()


if __name__ == "__main__":
    controller = database_controller()
    controller.drop_tables()
    controller.create_tables()
    controller.populate_tables()
