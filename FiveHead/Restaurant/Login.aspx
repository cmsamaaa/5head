<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FiveHead.Restaurant.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Staff Login | 5Head</title>
    <link rel="stylesheet" href="vendors/mdi/css/materialdesignicons.min.css" />
    <link rel="stylesheet" href="vendors/base/vendor.bundle.base.css" />
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/custom.css">
    <link rel="shortcut icon" href="images/favicon.png" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-scroller">
            <div class="container-fluid page-body-wrapper full-page-wrapper">
                <div class="content-wrapper d-flex align-items-stretch auth auth-img-bg">
                    <div class="row flex-grow">
                        <div class="col-lg-6 d-flex align-items-center justify-content-center">
                            <div class="auth-form-transparent text-left p-3">
                                <div class="brand-logo">
                                    <img src="images/logo-black.svg" alt="logo">
                                </div>
                                <h4>Welcome back!</h4>
                                <h6 class="font-weight-light">Happy to see you again!</h6>
                                <br />
                                <div class="form-group">
                                    <label for="tb_Username">Username</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend bg-transparent">
                                            <span class="input-group-text bg-transparent border-right-0">
                                                <i class="mdi mdi-account-outline text-primary"></i>
                                            </span>
                                        </div>
                                        <input runat="server" type="text" class="form-control form-control-lg border-left-0 login-field" id="tb_Username" placeholder="Username" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="tb_Password">Password</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend bg-transparent">
                                            <span class="input-group-text bg-transparent border-right-0">
                                                <i class="mdi mdi-lock-outline text-primary"></i>
                                            </span>
                                        </div>
                                        <input runat="server" type="password" class="form-control form-control-lg border-left-0 login-field" id="tb_Password" placeholder="Password" />
                                    </div>
                                </div>
                                <div class="my-3">
                                    <button runat="server" id="btn_Login" class="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn" type="submit" onserverclick="btn_Login_Click">SIGN IN</button>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 login-half-bg d-flex flex-row">
                            <p class="text-white font-weight-medium text-center flex-grow align-self-end">
                                Copyright &copy; 5Head
                                <script>document.write(new Date().getFullYear())</script>
                            </p>
                        </div>
                    </div>
                </div>
                <!-- content-wrapper ends -->
            </div>
            <!-- page-body-wrapper ends -->
        </div>
        <!-- container-scroller -->
        <!-- plugins:js -->
        <script src="vendors/base/vendor.bundle.base.js"></script>
        <!-- endinject -->
        <!-- inject:js -->
        <script src="js/off-canvas.js"></script>
        <script src="js/hoverable-collapse.js"></script>
        <script src="js/template.js"></script>
        <!-- endinject -->

        <script>
            $(document).ready(function () {
                RandomLoginImage();
            });

            function RandomLoginImage() {
                const bg = [
                    "url(images/login/galaxy-brain-1.png)",
                    "url(images/login/galaxy-brain-2.png)",
                    "url(images/login/galaxy-brain-3.png)"
                ];
                const i = Math.floor(Math.random() * 3);
                $(".auth .login-half-bg").css("background-image", bg[i])
            }
        </script>
    </form>
</body>
</html>
