"""
Brennan Miller-Klugman

Last Edited - 1/26/23

Overview - webapp.py is the flask portion of the web editor. 
"""

from flask import Flask, render_template, request
import database_populate

app = Flask(__name__)

controller = database_populate.database_controller()


@app.route("/", methods=["POST", "GET"])
def robots():
    if request.method == "POST":
        if "delete" in request.form:
            controller.delete(request.form["delete"], "robots")
        elif "update" in request.form:
            controller.update_robot_table(
                request.form["update"],
                request.form["name"],
                request.form["ip"],
                request.form["port"],
                request.form["image"],
            )
        elif "create" in request.form:
            controller.add_robot(
                request.form["name"],
                request.form["ip"],
                request.form["port"],
                request.form["image"],
                request.form["image_target_width"],
            )
    return render_template("robots.html")


@app.route("/robot_data")
def robot_data():
    return controller.getData("robots")


@app.route("/augments", methods=["POST", "GET"])
def augments():
    if request.method == "POST":
        if "delete" in request.form:
            controller.delete(request.form["delete"], "augments")
        elif "update" in request.form:
            controller.update_augment_table(
                request.form["update"],
                request.form["rob_id"],
                request.form["name"],
                request.form["location"],
                request.form["config"],
            )
        elif "create" in request.form:
            controller.add_augment(
                request.form["rob_id"],
                request.form["name"],
                request.form["location"],
                request.form["config"],
            )
    return render_template("augments.html")


@app.route("/augment_data")
def augment_data():
    return controller.getData("augments")


if __name__ == "__main__":
    app.run()
