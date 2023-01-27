import qrcode

class generate_tag:
    def __init__(self):
        self.mode = input("Select Moden \n Enter 1 to generate a robot tag \n Enter 2 to generate a robot tag and add it to the database")

        if self.mode == 1:
            self.robot_name = input("Enter a unique robot name")
            img = self.gen_qr(robot_name)
            img.save(f"self.robot_name {self.robot_name}")
            print(f"Generate tag saved to self.robot_name {self.robot_name}.")
        
        if self.mode == 2:
            self.robot_name = input("Enter a unique robot name: ")
            self.robot_ip = input("Enter ROS_TCP_Endpoint IP: ")
            self.robot_port = input("Enter ROS_TCP_Endpoint Port: ")

    def gen_qr(self):
        qr = qrcode.QRCode(
            version=1,
            error_correction=qrcode.constants.ERROR_CORRECT_L,
            box_size=10,
            border=0,
        )

        qr.add_data('Some data')
        qr.make(fit=True)

        return img = qr.make_image(fill_color="black", back_color="white")

        
if __name__=="__main__":
generate_tag()
