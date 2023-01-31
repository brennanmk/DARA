'''
Brennan Miller-Klugman

Last Edited - 1/26/23

Overview - webapp.py is the flask portion of the web editor. 
'''

from flask import Flask, render_template, request
import database_populate

app = Flask(__name__)

controller = database_populate.database_controller()

@app.route('/', methods = ['POST', 'GET'])
def overview():
    if request.method == 'POST':
        if 'delete' in request.form:
            controller.delete(request.form["delete"])
        elif 'update' in request.form:
            controller.update_table(request.form["update"], request.form["name"], request.form["ip"], request.form["port"], request.form["image"])
        elif 'create' in request.form:
            controller.add_to_table(request.form["name"], request.form["ip"], request.form["port"], request.form["image"])
    return render_template("overview.html")

@app.route('/robot_data')
def data():
    return controller.getData()
    
if __name__ == '__main__':
    app.run()