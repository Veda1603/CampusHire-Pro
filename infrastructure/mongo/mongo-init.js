// ============================================================================
// CampusHire Pro: MongoDB Initialization Script
// File: infrastructure/mongo/mongo-init.js
// ============================================================================

db = db.getSiblingDB('campushire_ai');

// 1. Create Collection: Resumes (Parsed Resume Data & Embeddings)
db.createCollection('resumes');
db.resumes.createIndex({ "student_id": 1 }, { unique: true });

// 2. Create Collection: Assessments (Online Coding Playground & MCQ Tests)
db.createCollection('assessments');
db.assessments.createIndex({ "job_id": 1 });

// 3. Create Collection: AI Interactions (Chatbot History & Logs)
db.createCollection('ai_interactions');
db.ai_interactions.createIndex({ "user_id": 1, "created_at": -1 });

// 4. Create Collection: Notifications (User Push Notifications)
db.createCollection('notifications');
db.notifications.createIndex({ "user_id": 1, "is_read": 1 });

print(">>> CampusHire Pro MongoDB collections & indexes successfully created! <<<");