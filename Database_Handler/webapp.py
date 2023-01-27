'''
Brennan Miller-Klugman

Last Edited - 1/26/23

Overview - webapp.py is the flask portion of the web editor. 
'''

from flask import Flask, render_template
import peewee
import database_populate

app = Flask(__name__)

@app.route('/')
def overview():
    return render_template("overview.html")

@app.route('/data')
def data():
    return database_populate.database_controller().getData()

if __name__ == '__main__':
   app.run()