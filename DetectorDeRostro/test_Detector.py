from unittest import TestCase


# class Test(TestCase):
#     def test_map(self):
#         self.assertTrue(True)
#
#
# class TestRostro(TestCase):
#     def test_malla(self):
#         self.assertTrue(True)


class TestTests(TestCase):
    def test_rostro(self):
        self.assertTrue(True)

#
# import time
# import cv2
# import mediapipe as mp
# import json
# mp_face_mesh = mp.solutions.face_mesh
# mp_drawing = mp.solutions.drawing_utils
# handsModule = mp.solutions.hands
# cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)
# rx = 0.0
# ry = 0.0
# rz = 0.0
# with mp_face_mesh.FaceMesh(static_image_mode=False, max_num_faces=1,min_detection_confidence=0.5) as face_mesh:
#     while True:
#         ret, frame = cap.read()
#         start = time.time()
#         if ret == False:
#             break
#         frame = cv2.flip(frame, 1)
#         frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
#         results = face_mesh.process(frame_rgb)
#         if results.multi_face_landmarks is not None:
#             for face_landmarks in results.multi_face_landmarks:
#                 for datos in handsModule.HandLandmark:
#                     mp_drawing.draw_landmarks(frame, face_landmarks,
#                     mp_face_mesh.FACEMESH_CONTOURS,
#                     mp_drawing.DrawingSpec(color=(0, 0, 0), thickness=1, circle_radius=1),
#                     mp_drawing.DrawingSpec(color=(255, 0, 0), thickness=1))
#                     normalizedLandmark = face_landmarks.landmark[datos]
#                     X = normalizedLandmark.x * 10
#                     y = normalizedLandmark.y * 10
#                     z = normalizedLandmark.z * 10
#         cv2.imshow("Detector", frame)
#         if cv2.waitKey(1) & 0xFF == ord('q'):
#             break
# cap.release()
# cv2.destroyAllWindows()
#
